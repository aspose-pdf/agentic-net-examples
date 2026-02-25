using System;
using System.IO;
using Aspose.Pdf;   // All SaveOptions (including HtmlSaveOptions) are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputHtml = "output.html";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure HTML conversion options
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Separate PDF marked‑content layers into distinct HTML <div> elements
                    ConvertMarkedContentToLayers = true,

                    // Example image handling – embed raster images as PNG inside SVG wrappers
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save as HTML using the explicit HtmlSaveOptions (required to get HTML output)
                pdfDoc.Save(outputHtml, htmlOpts);
                Console.WriteLine($"HTML saved to '{outputHtml}'.");
            }
        }
        // HTML conversion relies on GDI+ and is Windows‑only
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (DllNotFoundException)
        {
            Console.WriteLine("GDI+ (gdiplus.dll) not found. HTML conversion is Windows‑only.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}