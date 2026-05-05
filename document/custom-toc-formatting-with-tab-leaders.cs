using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Tagged;

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

        // Load the source PDF inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (correct namespace and API)
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Sample Document with Custom TOC");

            // -----------------------------------------------------------------
            // 1. Create a new page that will hold the Table of Contents (TOC)
            // -----------------------------------------------------------------
            Page tocPage = doc.Pages.Add();

            // -----------------------------------------------------------------
            // 2. Configure TOC information
            // -----------------------------------------------------------------
            TocInfo tocInfo = new TocInfo
            {
                // Title expects a TextFragment (see verified fix)
                Title = new TextFragment("Table of Contents"),
                // Hide page numbers for the whole TOC (per‑level hiding is not supported)
                IsShowPageNumbers = false,
                // Define how many heading levels we will format
                FormatArrayLength = 3
            };

            // Allocate the LevelFormat array
            tocInfo.FormatArray = new LevelFormat[tocInfo.FormatArrayLength];

            // Level 1 formatting (e.g., solid leader)
            LevelFormat level1 = new LevelFormat
            {
                LineDash = TabLeaderType.Solid,
                // Example margin: 20 points from left
                Margin = new MarginInfo { Left = 20 }
            };
            tocInfo.FormatArray[0] = level1;

            // Level 2 formatting (e.g., dash leader)
            LevelFormat level2 = new LevelFormat
            {
                LineDash = TabLeaderType.Dash,
                Margin = new MarginInfo { Left = 40 }
            };
            tocInfo.FormatArray[1] = level2;

            // Level 3 formatting (e.g., dot leader)
            LevelFormat level3 = new LevelFormat
            {
                LineDash = TabLeaderType.Dot,
                Margin = new MarginInfo { Left = 60 }
            };
            tocInfo.FormatArray[2] = level3;

            // Assign the TOC info to the page
            tocPage.TocInfo = tocInfo;

            // -----------------------------------------------------------------
            // 3. Add headings that will appear in the TOC
            // -----------------------------------------------------------------
            // Example: add three headings on separate pages
            for (int i = 1; i <= 3; i++)
            {
                // Ensure the target page exists
                Page page = doc.Pages[i];
                // Create a heading of the appropriate level (1, 2, or 3)
                Heading heading = new Heading(i) // level = i
                {
                    Text = $"Chapter {i}: Sample Heading Level {i}",
                    // Mark the heading to be included in the TOC
                    IsInList = true,
                    // Associate the heading with the TOC page we created
                    TocPage = tocPage,
                    // Enable automatic numbering (optional)
                    IsAutoSequence = true
                };
                // Add the heading to the page's paragraph collection
                page.Paragraphs.Add(heading);
            }

            // -----------------------------------------------------------------
            // 4. Save the modified document
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}