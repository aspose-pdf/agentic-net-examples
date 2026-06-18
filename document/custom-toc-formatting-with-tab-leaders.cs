using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_custom_toc.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has a page to host the TOC (use the first page)
            Page tocPage = doc.Pages[1];

            // Create TOC element via the tagged content API
            ITaggedContent tagged = doc.TaggedContent;
            TOCElement tocElement = tagged.CreateTOCElement();

            // Configure TOCInfo for the page
            TocInfo tocInfo = new TocInfo
            {
                // Title must be a TextFragment, not a plain string
                Title = new TextFragment("Table of Contents"),
                IsShowPageNumbers = true,          // Show page numbers by default
                IsCountTocPages = false            // Do not count TOC pages in page numbers
            };

            // Prepare LevelFormat array (one entry per TOC level)
            // Level 1 – solid leader, show page numbers
            LevelFormat level1 = new LevelFormat
            {
                LineDash = TabLeaderType.Solid,
                // Margin expects a MarginInfo object, not a List<float>
                Margin = new MarginInfo { Left = 0f },
                TextState = new TextState { FontSize = 14, Font = FontRepository.FindFont("Helvetica") }
            };

            // Level 2 – no leader, hide page numbers (will be handled later)
            LevelFormat level2 = new LevelFormat
            {
                LineDash = TabLeaderType.None, // No leader for this level
                Margin = new MarginInfo { Left = 20f },
                TextState = new TextState { FontSize = 12, Font = FontRepository.FindFont("Helvetica") }
            };

            // Assign the format array to the TOCInfo
            tocInfo.FormatArray = new LevelFormat[] { level1, level2 };
            tocInfo.FormatArrayLength = tocInfo.FormatArray.Length;

            // Disable page numbers for level 2 by turning off the global flag.
            // Since per‑level control is not available, we hide all page numbers here.
            tocInfo.IsShowPageNumbers = false;

            // Attach the TOCInfo to the page
            tocPage.TocInfo = tocInfo;

            // Add the TOC element to the document structure (as a child of the root)
            StructureElement root = tagged.RootElement;
            root.AppendChild(tocElement);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom TOC formatting: {outputPath}");
    }
}
