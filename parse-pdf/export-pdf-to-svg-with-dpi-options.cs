using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        // Input PDF and output SVG paths
        const string inputPdf = "input.pdf";
        const string outputSvg = "output.svg";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block (ensures proper disposal)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // -----------------------------------------------------------------
            // Configure SVG save options.
            //  • ScaleToPixels – converts typographic points to pixel units,
            //    which effectively changes the DPI of the resulting SVG.
            //  • IsMultiThreading – enables parallel processing of pages for
            //    faster conversion when dealing with multi‑page PDFs.
            //  • CssStyleSheet – not available in the current Aspose.Pdf version;
            //    CSS can be added manually to the generated SVG if required.
            // -----------------------------------------------------------------
            SvgSaveOptions svgOptions = new SvgSaveOptions
            {
                ScaleToPixels = true,               // Use pixel units (higher DPI)
                IsMultiThreading = true            // Faster processing on multi‑core CPUs
                // Note: CssStyleSheet property is unavailable in this SDK version.
                // To apply custom CSS, edit the generated SVG file after saving.
            };

            // Save the entire PDF as an SVG file using the configured options.
            pdfDoc.Save(outputSvg, svgOptions);
            Console.WriteLine($"PDF saved as SVG with custom options: {outputSvg}");

            // -----------------------------------------------------------------
            // OPTIONAL: Extract vector graphics from a specific page using
            // SvgExtractor with custom extraction options (e.g., DPI‑related
            // settings such as MinStrokeWidth).
            // -----------------------------------------------------------------
            int pageNumber = 1; // 1‑based index
            if (pageNumber <= pdfDoc.Pages.Count)
            {
                Page page = pdfDoc.Pages[pageNumber];

                // Configure extraction options.
                SvgExtractionOptions extractionOpts = new SvgExtractionOptions
                {
                    // Increase minimum stroke width to ensure thin lines are visible
                    // when rendered at higher DPI.
                    MinStrokeWidth = 0.8f,
                    // Disable strict bounding‑box checks to include partially visible
                    // elements.
                    StrictExtractionAreaBoundCheck = false
                };

                // Create the extractor with the custom options.
                SvgExtractor extractor = new SvgExtractor(extractionOpts);

                // Extract all vector graphics on the page to separate SVG files
                // inside the specified directory.
                string extractionDir = "ExtractedSvgs";
                Directory.CreateDirectory(extractionDir);
                extractor.Extract(page, extractionDir);
                Console.WriteLine($"Vector graphics extracted to folder: {extractionDir}");
            }
        }
    }
}
