using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // not required here but kept for completeness

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Output HTML file path
        const string htmlPath = "output.html";

        // Verify the input file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // Open the PDF document inside a using block (ensures disposal)
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Initialize HtmlSaveOptions – required to produce HTML output
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    // Example: embed all resources (images, CSS) into the single HTML file
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    // Example: embed raster images as PNGs inside SVG wrappers
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save the PDF as HTML using the options
                // This must be wrapped in a try‑catch because HTML conversion relies on GDI+ (Windows only)
                try
                {
                    pdfDocument.Save(htmlPath, htmlOptions);
                    Console.WriteLine($"PDF successfully converted to HTML: '{htmlPath}'.");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows GDI+. Skipped on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}