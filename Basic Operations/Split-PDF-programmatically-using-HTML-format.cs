using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string htmlOutput = "output.html";

        // Verify input file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Load PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Configure HTML conversion to split each PDF page into its own HTML file
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    SplitIntoPages = true,
                    // Embed raster images as PNG inside SVG (cross‑platform safe option)
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save will produce multiple HTML files (one per page)
                doc.Save(htmlOutput, htmlOpts);
                Console.WriteLine("PDF successfully split into separate HTML pages.");
            }
        }
        // HTML conversion relies on GDI+ and is Windows‑only; handle gracefully on other platforms
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML conversion requires Windows GDI+. Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}