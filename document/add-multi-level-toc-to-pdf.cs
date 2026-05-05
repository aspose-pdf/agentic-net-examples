using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.LogicalStructure; // only for structure types if needed (not used here)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_toc.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the existing PDF inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Insert a new page at the beginning to hold the TOC
            // Pages are 1‑based, so index 1 inserts before the current first page
            Page tocPage = doc.Pages.Insert(1);

            // Create TOC information and assign it to the new page
            TocInfo tocInfo = new TocInfo();

            // Title of the TOC (TextFragment is required)
            TextFragment titleFragment = new TextFragment("Table of Contents")
            {
                TextState = { FontSize = 20, FontStyle = FontStyles.Bold }
            };
            tocInfo.Title = titleFragment;

            // Show page numbers in the TOC (default is true)
            tocInfo.IsShowPageNumbers = true;

            // Optional: prefix before page numbers (e.g., "p.")
            tocInfo.PageNumbersPrefix = "p.";

            // Assign the TOC info to the page
            tocPage.TocInfo = tocInfo;

            // -----------------------------------------------------------------
            // Sample data: headings to appear in the TOC.
            // In a real scenario you would detect headings automatically or
            // read them from some source. Here we create three sample headings.
            // -----------------------------------------------------------------
            var headings = new[]
            {
                new { Level = 1, Text = "Chapter 1 – Introduction",      PageNumber = 2 },
                new { Level = 2, Text = "Section 1.1 – Background",      PageNumber = 3 },
                new { Level = 1, Text = "Chapter 2 – Methodology",       PageNumber = 5 },
                new { Level = 2, Text = "Section 2.1 – Data Collection", PageNumber = 6 },
                new { Level = 2, Text = "Section 2.2 – Analysis",        PageNumber = 7 },
                new { Level = 1, Text = "Chapter 3 – Results",           PageNumber = 9 }
            };

            // Add each heading to the TOC page
            foreach (var h in headings)
            {
                // Ensure the target page exists
                if (h.PageNumber < 1 || h.PageNumber > doc.Pages.Count)
                    continue; // skip invalid entries

                // Create a heading of the required level
                Heading heading = new Heading(h.Level)
                {
                    Text = h.Text,
                    // Link the heading to the target page
                    DestinationPage = doc.Pages[h.PageNumber],
                    // The top position is the height of the target page (so the link points to the top)
                    Top = doc.Pages[h.PageNumber].PageInfo.Height,
                    // Associate this heading with the TOC page we created
                    TocPage = tocPage,
                    // Enable automatic numbering in the TOC
                    IsInList = true,
                    IsAutoSequence = true
                };

                // Add the heading to the TOC page's paragraph collection
                tocPage.Paragraphs.Add(heading);
            }

            // Save the modified document (lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with Table of Contents saved to '{outputPath}'.");
    }
}