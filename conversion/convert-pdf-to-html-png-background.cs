using System;
using System.IO;
using Aspose.Pdf;                     // All SaveOptions are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputHtmlPath = "output.html";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Configure HTML conversion options:
            // - Embed all resources into a single HTML file
            // - Render all raster images (including background images) as PNGs embedded into SVG
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };

            // HTML conversion requires GDI+ (Windows only). Wrap in try‑catch to handle other platforms.
            try
            {
                pdfDoc.Save(outputHtmlPath, htmlOpts);
                Console.WriteLine($"HTML saved to '{outputHtmlPath}'.");
            }
            catch (TypeInitializationException)
            {
                Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
            }
            catch (DllNotFoundException)
            {
                Console.WriteLine("GDI+ library not found. HTML conversion is unavailable on this platform.");
            }
        }
    }
}