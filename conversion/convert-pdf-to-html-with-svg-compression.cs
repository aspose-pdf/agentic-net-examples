using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputHtmlPath = "output.html";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Configure HTML conversion options
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                // Embed all resources into a single HTML file
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                // Save raster images as PNGs embedded into SVG (optional, improves compatibility)
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                // CompressSvgImages property is not available in the referenced Aspose.Pdf version; omitted.
            };

            // HTML conversion uses GDI+ and is Windows‑only; handle possible platform exceptions
            try
            {
                pdfDoc.Save(outputHtmlPath, htmlOpts);
                Console.WriteLine($"PDF successfully converted to HTML: {outputHtmlPath}");
            }
            catch (TypeInitializationException)
            {
                Console.WriteLine("HTML conversion requires GDI+ (Windows). Skipped on this platform.");
            }
            catch (DllNotFoundException)
            {
                Console.WriteLine("GDI+ library not found. HTML conversion is unavailable on this platform.");
            }
        }
    }
}
