using System;
using System.IO;
using Aspose.Pdf; // HtmlSaveOptions is in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.html";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Configure HTML conversion options
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Preserve transparent text and shadowed text as transparent
                    SaveTransparentTexts               = true,
                    SaveShadowedTextsAsTransparentTexts = true,

                    // Optional: embed all resources into the HTML file
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save as HTML using the explicit HtmlSaveOptions (required for non‑PDF output)
                doc.Save(outputPath, htmlOpts);
                Console.WriteLine($"HTML saved to '{outputPath}'.");
            }
        }
        // HTML conversion uses GDI+ and is Windows‑only; handle cross‑platform scenarios
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}