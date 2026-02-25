using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath    = "input.pdf";
        const string htmlOutput = "output.html";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Configure HTML conversion to split each PDF page into a separate HTML file
                HtmlSaveOptions opts = new HtmlSaveOptions
                {
                    SplitIntoPages = true,
                    // Embed raster images as PNGs inside SVG wrappers (cross‑platform safe)
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // HTML conversion requires GDI+ on Windows; catch the exception on other platforms
                try
                {
                    doc.Save(htmlOutput, opts);
                    Console.WriteLine($"PDF split to HTML pages saved to '{htmlOutput}'.");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}