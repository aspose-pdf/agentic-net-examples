using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "source.pdf";      // existing PDF with content
        const string outputPath = "output_with_toc.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing document
        using (Document doc = new Document(inputPath))
        {
            // Insert a new page at the beginning to hold the TOC
            doc.Pages.Insert(1);
            Page tocPage = doc.Pages[1];

            // Create and assign TocInfo to the TOC page
            TocInfo tocInfo = new TocInfo();
            tocInfo.Title = new TextFragment("Table of Contents");
            tocPage.TocInfo = tocInfo;

            // Configure TOC formatting for three levels
            tocInfo.FormatArrayLength = 3;
            tocInfo.FormatArray = new LevelFormat[3];

            // Level 1: solid leader, show page numbers
            tocInfo.FormatArray[0] = new LevelFormat
            {
                LineDash = TabLeaderType.Solid
            };

            // Level 2: dash leader, hide page numbers for this level
            tocInfo.FormatArray[1] = new LevelFormat
            {
                LineDash = TabLeaderType.Dash
            };
            // Hide page numbers for level 2 by disabling the leader (no tab stop)
            // This effectively removes the page number column for this level
            tocInfo.FormatArray[1].LineDash = TabLeaderType.None;

            // Level 3: dot leader, show page numbers
            tocInfo.FormatArray[2] = new LevelFormat
            {
                LineDash = TabLeaderType.Dot
            };

            // Global setting: show page numbers (will be suppressed for level 2 by the above setting)
            tocInfo.IsShowPageNumbers = true;

            // Add headings that will appear in the TOC
            // Start from the page after the TOC page (index 2)
            for (int i = 2; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Example: add a level 1 heading on every odd page
                if ((i % 2) == 0)
                {
                    Heading heading = new Heading(1);
                    heading.Text = $"Chapter {(i / 2)}";
                    heading.IsInList = true;               // include in TOC
                    heading.TocPage = tocPage;             // associate with TOC page
                    heading.DestinationPage = page;        // link target
                    heading.Top = page.Rect.Height;        // position at top of page
                    page.Paragraphs.Add(heading);
                }

                // Example: add a level 2 heading on every even page
                if ((i % 2) == 1)
                {
                    Heading subHeading = new Heading(2);
                    subHeading.Text = $"Section {(i - 1) / 2}";
                    subHeading.IsInList = true;
                    subHeading.TocPage = tocPage;
                    subHeading.DestinationPage = page;
                    subHeading.Top = page.Rect.Height;
                    page.Paragraphs.Add(subHeading);
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with customized TOC: {outputPath}");
    }
}