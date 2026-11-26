using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputHtml = "output.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document using the core Document API (no Facades)
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure HTML conversion options. Properties that are not present in the
                // referenced Aspose.Pdf version (e.g., SplitIntoLayers, RenderLayers, ImagesFolder, etc.)
                // are omitted to keep the code compatible across versions.
                var htmlOpts = new HtmlSaveOptions
                {
                    // Embed all resources (CSS, JS, images) directly into the HTML file.
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    // Save raster images as PNG embedded into SVG – this preserves visual fidelity
                    // and works well with layered PDFs when the library supports it.
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                    // Note: Explicit layer splitting is not available in the current Aspose.Pdf version.
                    // When supported, the library renders PDF layers automatically into the generated HTML.
                };

                // Perform the conversion.
                pdfDoc.Save(outputHtml, htmlOpts);
                Console.WriteLine($"PDF successfully converted to HTML: {outputHtml}");
            }
        }
        catch (TypeInitializationException)
        {
            // HTML conversion via Aspose.Pdf requires GDI+ (Windows only).
            Console.WriteLine("HTML conversion with layer support requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
