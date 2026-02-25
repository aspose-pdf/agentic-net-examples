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
                // Configure HTML save options to split each PDF page into a separate HTML file
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    SplitIntoPages = true,
                    // Embed raster images as PNGs inside SVG to keep output self‑contained
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // HTML conversion relies on GDI+ (Windows only); handle platform limitations gracefully
                try
                {
                    doc.Save(htmlOutput, htmlOptions);
                    Console.WriteLine("PDF successfully split into HTML pages.");
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