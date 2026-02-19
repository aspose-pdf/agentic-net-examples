using System;
using System.IO;
using Aspose.Pdf;

class PdfToHtmlWithSvgCompression
{
    static void Main(string[] args)
    {
        // Input PDF and output HTML paths (can be passed as arguments or hard‑coded)
        string inputPdfPath = args.Length > 0 ? args[0] : "input.pdf";
        string outputHtmlPath = args.Length > 1 ? args[1] : "output.html";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Configure HTML conversion options
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                // Compress generated SVG graphics (creates .svgz files)
                CompressSvgGraphicsIfAny = true,

                // Embed raster images inside SVG wrappers (optional, but works well with compression)
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,

                // Use fixed layout to preserve original appearance (adjust as needed)
                FixedLayout = true
            };

            // Save the PDF as HTML using the configured options
            pdfDocument.Save(outputHtmlPath, htmlOptions);
            Console.WriteLine($"Conversion completed successfully. HTML saved to '{outputHtmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}
