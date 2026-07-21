using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputRoot = "Subpaths";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the root output directory exists
        Directory.CreateDirectory(outputRoot);

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Configure extraction to generate a separate SVG for each subpath
            SvgExtractionOptions options = new SvgExtractionOptions
            {
                ExtractEverySubPathToSvg = true
                // SVG files are transparent by default; no extra settings needed
            };

            // Create the extractor with the configured options
            SvgExtractor extractor = new SvgExtractor(options);

            // Iterate through all pages
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a subdirectory for each page to avoid filename collisions
                string pageDir = Path.Combine(outputRoot, $"Page_{i}");
                Directory.CreateDirectory(pageDir);

                // Extract each subpath on the page to its own SVG file
                extractor.Extract(page, pageDir);
            }
        }

        Console.WriteLine("All subpaths have been exported as separate SVG images.");
    }
}