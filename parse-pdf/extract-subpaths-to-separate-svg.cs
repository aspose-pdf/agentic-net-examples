using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";

        // Directory where each subpath SVG will be saved
        const string outputRoot = "Subpaths";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the root output directory exists
        Directory.CreateDirectory(outputRoot);

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure extraction to create a separate SVG for every subpath
            SvgExtractionOptions extractionOptions = new SvgExtractionOptions
            {
                ExtractEverySubPathToSvg = true
            };

            // Create the extractor with the above options
            SvgExtractor extractor = new SvgExtractor(extractionOptions);

            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // Create a subdirectory for the current page's SVG files
                string pageOutputDir = Path.Combine(outputRoot, $"Page_{pageIndex}");
                Directory.CreateDirectory(pageOutputDir);

                // Extract each subpath on the page to a separate SVG file
                // The method creates one SVG file per subpath inside the specified directory
                extractor.Extract(page, pageOutputDir);
            }
        }

        Console.WriteLine("Subpath extraction completed.");
    }
}