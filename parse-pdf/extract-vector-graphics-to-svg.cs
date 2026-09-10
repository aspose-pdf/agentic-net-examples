using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputRoot = "VectorGraphics";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Create root folder for extracted SVG files
        Directory.CreateDirectory(outputRoot);

        // Load PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Configure extractor to generate a separate SVG for each sub‑path
            SvgExtractionOptions extractionOptions = new SvgExtractionOptions {
                ExtractEverySubPathToSvg = true
            };
            SvgExtractor extractor = new SvgExtractor(extractionOptions);

            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a sub‑directory for the current page's SVG files
                string pageDir = Path.Combine(outputRoot, $"Page_{i}");
                Directory.CreateDirectory(pageDir);

                // Extract all vector graphics from the page into individual SVG files
                extractor.Extract(page, pageDir);
            }
        }

        Console.WriteLine("Vector graphics extraction completed.");
    }
}