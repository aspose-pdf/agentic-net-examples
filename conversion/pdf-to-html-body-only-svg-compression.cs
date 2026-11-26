using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string htmlPath = "output.html";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Configure HTML conversion options compatible with all supported Aspose.Pdf versions
                var htmlOpts = new HtmlSaveOptions
                {
                    // Generate only the <body> fragment (no <html>/<head> tags)
                    HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteOnlyBodyContent,
                    // Embed raster images as PNG inside SVG to keep the output self‑contained
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                    // Do not embed external parts – keeps the markup minimal
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.NoEmbedding,
                    // Produce a single HTML file rather than splitting into pages
                    SplitIntoPages = false
                };

                // HTML conversion relies on GDI+ (Windows only); handle platform limitations
                try
                {
                    doc.Save(htmlPath, htmlOpts);
                    Console.WriteLine($"HTML saved to '{htmlPath}'.");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
                }
                catch (DllNotFoundException)
                {
                    Console.WriteLine("GDI+ library not found. HTML conversion is unavailable on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
