using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        // Input PDF containing vector graphics.
        const string inputPdf = "input.pdf";

        // Output SVG file for the whole document.
        const string outputSvg = "output.svg";

        // Directory to store extracted SVG graphics per page.
        const string extractionDir = "ExtractedSvgs";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the extraction directory exists.
        Directory.CreateDirectory(extractionDir);

        // -----------------------------------------------------------------
        // 1. Export the entire PDF to a single SVG file with custom options.
        // -----------------------------------------------------------------
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure SVG save options.
            // ScaleToPixels converts typographic points to pixel units (useful for DPI control).
            // IsMultiThreading enables parallel processing of pages for faster conversion.
            SvgSaveOptions svgOptions = new SvgSaveOptions
            {
                ScaleToPixels = true,
                IsMultiThreading = true
                // Additional options such as CacheGlyphs can be set here if needed.
            };

            // Save the whole document as SVG using the configured options.
            pdfDoc.Save(outputSvg, svgOptions);
        }

        // -----------------------------------------------------------------
        // 2. Extract vector graphics from each page using SvgExtractor with
        //    custom extraction options (e.g., stroke width, grouping, DPI).
        // -----------------------------------------------------------------
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Prepare extraction options.
            SvgExtractionOptions extractionOptions = new SvgExtractionOptions
            {
                // Do not automatically group subpaths; each subpath will be a separate SVG.
                AutoGrouping = false,

                // Minimum stroke width in the resulting SVG (helps when original PDF uses very thin lines).
                MinStrokeWidth = 0.5,

                // When true, only subpaths completely inside ExtractionAreaBound are kept.
                StrictExtractionAreaBoundCheck = false
            };

            // Create an extractor with the above options.
            SvgExtractor extractor = new SvgExtractor(extractionOptions);

            // Iterate through all pages and extract their vector graphics.
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                Page page = pdfDoc.Pages[pageNum];

                // Extract all SVG images from the current page to separate files.
                // The method creates one SVG file per vector graphic found on the page.
                string pageDir = Path.Combine(extractionDir, $"Page_{pageNum}");
                Directory.CreateDirectory(pageDir);
                extractor.Extract(page, pageDir);
            }
        }

        Console.WriteLine("SVG export and vector graphic extraction completed.");
    }
}