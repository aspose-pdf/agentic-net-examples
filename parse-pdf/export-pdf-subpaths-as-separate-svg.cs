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

        // Root directory where subpath SVG images will be saved
        const string outputRoot = "Subpaths";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the root output directory exists
        Directory.CreateDirectory(outputRoot);

        // Configure extraction options to export each subpath as a separate SVG
        SvgExtractionOptions extractionOptions = new SvgExtractionOptions {
            // Enable extraction of every subpath into its own SVG file
            ExtractEverySubPathToSvg = true,
            // Disable automatic grouping to keep subpaths separate
            AutoGrouping = false
        };

        // Create the SVG extractor with the configured options
        SvgExtractor svgExtractor = new SvgExtractor(extractionOptions);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // Create a subdirectory for the current page's SVG files
                string pageOutputDir = Path.Combine(outputRoot, $"Page_{pageIndex}");
                Directory.CreateDirectory(pageOutputDir);

                // Extract each subpath on the page to a separate SVG file
                // The method creates one SVG file per subpath inside the specified directory
                svgExtractor.Extract(page, pageOutputDir);
            }
        }

        Console.WriteLine($"Extraction completed. SVG files are saved under '{outputRoot}'.");
    }
}