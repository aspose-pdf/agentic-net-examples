using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";
        // Output HTML file (or base name when splitting)
        const string htmlOutput = "output.html";

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Configure HTML save options to split each PDF page into a separate HTML file
                HtmlSaveOptions opts = new HtmlSaveOptions
                {
                    SplitIntoPages = true,
                    // Embed raster images as PNG inside SVG to keep a single HTML file per page
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                try
                {
                    // Save the document as HTML using the configured options
                    doc.Save(htmlOutput, opts);
                    Console.WriteLine("Split to HTML pages.");
                }
                catch (TypeInitializationException)
                {
                    // HTML conversion requires GDI+ (Windows only); handle non‑Windows platforms gracefully
                    Console.WriteLine("HTML requires Windows (GDI+). Skipped.");
                }
            }
        }
        catch (Exception ex)
        {
            // General error handling
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}