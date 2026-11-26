using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";
        const string outputDir = "HtmlPages";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Configure HTML conversion options
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Create a separate HTML file for each PDF page
                    SplitIntoPages = true,

                    // Embed raster images as PNG inside generated SVG fragments
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,

                    // Compress generated SVG images (property exists in recent versions)
                    // If the property is unavailable, remove or comment it out.
                    //CompressSvgImages = true
                };

                // Base file name for the generated HTML files
                string baseHtmlPath = Path.Combine(outputDir, "page.html");

                // HTML conversion may require GDI+ (Windows only); handle cross‑platform gracefully
                try
                {
                    doc.Save(baseHtmlPath, htmlOpts);
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipping on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}