using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // TabLeaderType enum

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page to host the TOC
            Page tocPage = doc.Pages[1];

            // Create and configure the TocInfo object
            TocInfo tocInfo = new TocInfo
            {
                // Disable page numbers for the entire TOC.
                IsShowPageNumbers = false,

                // Title must be a TextFragment, not a plain string.
                Title = new TextFragment("Table of Contents")
            };

            // Define formatting for three TOC levels
            LevelFormat level1 = new LevelFormat
            {
                // Solid leader (dots or dashes) between entry text and page number
                LineDash = TabLeaderType.Solid
            };

            LevelFormat level2 = new LevelFormat
            {
                // No leader – useful when page numbers are hidden for this level
                LineDash = TabLeaderType.None
            };

            LevelFormat level3 = new LevelFormat
            {
                // Dotted leader for deeper hierarchy
                LineDash = TabLeaderType.Dot
            };

            // Assign the level formats to the TOCInfo
            tocInfo.FormatArray = new LevelFormat[] { level1, level2, level3 };
            tocInfo.FormatArrayLength = tocInfo.FormatArray.Length;

            // Attach the TOCInfo to the page that will contain the TOC
            tocPage.TocInfo = tocInfo;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with customized TOC saved to '{outputPath}'.");
    }
}
