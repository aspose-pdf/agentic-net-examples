using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        // Input PDF file
        string inputPath = "input.pdf";
        // Directory where SVG files will be written
        string outputDir = "SvgOutput";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Verify that page 2 exists (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document does not contain a page 2.");
                return;
            }

            // Select page 2
            Page page = doc.Pages[2];

            // Configure SVG extraction options – enable a separate SVG file for each sub‑path
            var extractionOptions = new SvgExtractionOptions
            {
                ExtractEverySubPathToSvg = true
            };

            // Create the extractor with the configured options
            var extractor = new SvgExtractor(extractionOptions);

            // Base file name (the extractor will append an index for each sub‑path)
            string baseFilePath = Path.Combine(outputDir, "page2");

            // Perform the extraction. The method creates files like "page2_1.svg", "page2_2.svg", …
            extractor.Extract(page, baseFilePath);
        }

        Console.WriteLine("SVG extraction completed.");
    }
}
