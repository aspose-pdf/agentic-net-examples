using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Directory where extracted SVG subpaths will be saved
        const string outputRoot = "ExtractedSubpaths";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output root directory exists
        Directory.CreateDirectory(outputRoot);

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure extraction options to export each subpath as a separate SVG
            SvgExtractionOptions extractionOptions = new SvgExtractionOptions
            {
                ExtractEverySubPathToSvg = true
            };

            // Create the extractor with the configured options
            SvgExtractor extractor = new SvgExtractor(extractionOptions);

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // Create a subdirectory for the current page's SVG files
                string pageOutputDir = Path.Combine(outputRoot, $"Page_{pageIndex}");
                Directory.CreateDirectory(pageOutputDir);

                // Extract each subpath on the page to a separate SVG file
                // The extractor will generate one SVG file per subpath because
                // ExtractEverySubPathToSvg is set to true.
                extractor.Extract(page, pageOutputDir);
            }
        }

        Console.WriteLine("Subpath extraction completed.");
    }
}