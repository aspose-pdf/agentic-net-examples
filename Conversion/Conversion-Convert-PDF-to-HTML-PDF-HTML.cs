using System;
using System.IO;
using Aspose.Pdf; // HtmlSaveOptions is in this namespace

class PdfToHtmlConverter
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

        // Wrap Document in a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Initialize HtmlSaveOptions (required for non‑PDF output)
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                // Example setting: embed raster images as PNG inside SVG
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };

            // HTML conversion relies on GDI+ (Windows only). Catch platform‑specific exceptions.
            try
            {
                pdfDocument.Save(outputHtmlPath, htmlOptions);
                Console.WriteLine($"PDF successfully converted to HTML: {outputHtmlPath}");
            }
            catch (TypeInitializationException)
            {
                Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
            }
            catch (DllNotFoundException)
            {
                Console.WriteLine("GDI+ library not found. HTML conversion is Windows‑only.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Conversion failed: {ex.Message}");
            }
        }
    }
}