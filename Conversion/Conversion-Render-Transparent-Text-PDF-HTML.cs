using System;
using System.IO;
using Aspose.Pdf;

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
            // Load PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPath))
            {
                // Configure HTML save options to preserve transparent and shadowed texts
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    SaveTransparentTexts = true,
                    SaveShadowedTextsAsTransparentTexts = true,
                    // Optional: embed raster images as PNG inside SVG for better compatibility
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // HTML conversion relies on GDI+ (Windows only). Wrap in try‑catch for cross‑platform safety.
                try
                {
                    pdfDoc.Save(outputPath, htmlOpts);
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