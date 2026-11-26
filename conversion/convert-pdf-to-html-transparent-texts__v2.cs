using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for HtmlSaveOptions

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputHtml = "output.html";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Configure HTML conversion with full text fidelity
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Preserve transparent text rendering
                    SaveTransparentTexts = true,
                    // Render shadowed text as transparent to keep appearance
                    SaveShadowedTextsAsTransparentTexts = true,
                    // Embed all resources into the HTML file
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    // Save raster images as PNGs embedded in SVG for better quality
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save as HTML using the explicit options (extension alone is ignored)
                doc.Save(outputHtml, htmlOpts);
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
            Console.WriteLine("GDI+ library not found. HTML conversion is Windows‑only.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}