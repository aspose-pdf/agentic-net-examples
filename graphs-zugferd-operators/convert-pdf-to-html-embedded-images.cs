using System;
using System.IO;
using Aspose.Pdf;                     // Core API
using Aspose.Pdf.Facades;            // For potential custom handling (not required here)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputHtml = "output.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure HTML save options:
            // - Embed all resources (CSS, fonts, images) directly into the HTML file.
            // - Store raster images as PNGs embedded into SVG wrappers as Base64 strings.
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };

            // Save to HTML. On non‑Windows platforms this may throw a TypeInitializationException
            // because HTML conversion relies on GDI+. Wrap it to avoid crashes.
            try
            {
                pdfDoc.Save(outputHtml, htmlOpts);
                Console.WriteLine($"PDF successfully converted to HTML: '{outputHtml}'");
            }
            catch (TypeInitializationException)
            {
                Console.WriteLine("HTML conversion requires Windows GDI+. Skipped on this platform.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error during conversion: {ex.Message}");
            }
        }
    }
}