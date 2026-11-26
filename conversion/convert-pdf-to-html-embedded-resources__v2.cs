using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "Layers.html";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document.
            using (Document doc = new Document(inputPath))
            {
                // HtmlSaveOptions – properties that exist in the referenced Aspose.Pdf version.
                // The older version does not expose SplitIntoLayers (or RenderLayers), so we omit it.
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Embed all resources (CSS, JS, images) directly into the HTML file.
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    // Save raster images as PNG data‑URI strings embedded inside SVG elements.
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // HTML conversion uses GDI+ and is Windows‑only.
                try
                {
                    doc.Save(outputPath, htmlOpts);
                    Console.WriteLine($"HTML saved to '{outputPath}'.");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
