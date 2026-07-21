using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputSvg = "output.svg";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure SVG save options.
            // ScaleToPixels converts typographic points to pixels, effectively controlling DPI.
            // IsMultiThreading enables parallel processing for faster conversion.
            // CacheGlyphs improves performance when many fonts are used.
            SvgSaveOptions svgOptions = new SvgSaveOptions
            {
                ScaleToPixels = true,
                IsMultiThreading = true,
                CacheGlyphs = true
            };

            // Save the entire document as an SVG file using the custom options.
            pdfDoc.Save(outputSvg, svgOptions);
            Console.WriteLine($"Document saved as SVG to '{outputSvg}'.");

            // OPTIONAL: Extract vector graphics from the first page with custom extraction settings.
            if (pdfDoc.Pages.Count > 0)
            {
                Page page = pdfDoc.Pages[1];

                // Set up extraction options (e.g., minimum stroke width, automatic grouping).
                SvgExtractionOptions extractionOpts = new SvgExtractionOptions
                {
                    MinStrokeWidth = 0.8,   // enforce a minimum stroke width in the resulting SVG.
                    AutoGrouping = true,    // let the extractor group subpaths automatically.
                    GroupStrength = 0.9     // stronger grouping for cleaner SVG output.
                };

                // Create an extractor using the defined options.
                SvgExtractor extractor = new SvgExtractor(extractionOpts);

                // Extract each vector graphic on the page to separate SVG files in a folder.
                string graphicsFolder = "PageGraphics";
                Directory.CreateDirectory(graphicsFolder);
                extractor.Extract(page, graphicsFolder);
                Console.WriteLine($"Vector graphics from page 1 extracted to folder '{graphicsFolder}'.");
            }
        }
    }
}