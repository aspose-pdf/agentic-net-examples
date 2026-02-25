using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions, including HtmlSaveOptions, are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputHtmlPath = "output.html";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Initialize HtmlSaveOptions (required to produce HTML output)
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Example setting: embed raster images as PNG inside SVG wrappers
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save as HTML using the options (extension alone is not enough)
                pdfDoc.Save(outputHtmlPath, htmlOpts);
            }

            Console.WriteLine($"PDF successfully converted to HTML: '{outputHtmlPath}'");
        }
        // HTML conversion relies on GDI+ and is Windows‑only
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}