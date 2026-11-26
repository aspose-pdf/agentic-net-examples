using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputHtml = "output.html";

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Configure HTML conversion:
                // - SplitIntoPages creates a separate HTML file per PDF page
                // - RasterImagesSavingMode ensures raster images are embedded as PNGs within SVG
                // - The property CompressSvgGraphics does not exist in the referenced Aspose.Pdf version;
                //   the SDK compresses SVGs by default, so we simply omit the unsupported member.
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    SplitIntoPages = true,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save as HTML using the explicit options (required to get HTML output)
                doc.Save(outputHtml, htmlOpts);
                Console.WriteLine($"HTML saved to '{outputHtml}' (pages split).");
            }
        }
        // HTML conversion relies on GDI+ and is Windows‑only
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
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
