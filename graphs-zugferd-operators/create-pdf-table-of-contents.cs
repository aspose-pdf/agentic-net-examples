using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_toc.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Insert a new page at the beginning to hold the Table of Contents
            doc.Pages.Insert(1);
            Page tocPage = doc.Pages[1];

            // Create TOC information and assign a title
            TocInfo tocInfo = new TocInfo();
            TextFragment tocTitle = new TextFragment("Table of Contents")
            {
                // Optional formatting for the title
                TextState = { FontSize = 20, FontStyle = FontStyles.Bold, ForegroundColor = Aspose.Pdf.Color.DarkBlue }
            };
            tocInfo.Title = tocTitle;
            tocInfo.IsShowPageNumbers = true;   // show page numbers in the TOC
            tocInfo.IsCountTocPages = false;    // do not count the TOC page itself
            tocPage.TocInfo = tocInfo;

            // For demonstration, create a heading on each existing page (except the TOC page)
            // and link it to the TOC page. In a real scenario you would detect actual headings.
            for (int i = 2; i <= doc.Pages.Count; i++)
            {
                // Create a heading of level 1
                Heading heading = new Heading(1);

                // Associate the heading with the TOC page
                heading.TocPage = tocPage;

                // Destination page where the heading resides
                heading.DestinationPage = doc.Pages[i];

                // Position the heading at the top of the page (Y coordinate)
                heading.Top = doc.Pages[i].PageInfo.Height - 50; // 50 points margin from top

                // Add the visible text for the heading
                TextSegment segment = new TextSegment($"Chapter {i - 1}");
                heading.Segments.Add(segment);

                // Add the heading to the page's paragraph collection
                doc.Pages[i].Paragraphs.Add(heading);
            }

            // Save the updated document with the TOC page
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with Table of Contents saved to '{outputPath}'.");
    }
}