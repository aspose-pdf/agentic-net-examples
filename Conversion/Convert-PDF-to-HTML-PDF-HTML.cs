using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";
        // Desired output HTML file path
        const string htmlPath = "output.html";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: create & load)
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Initialize HtmlSaveOptions (required for non‑PDF output)
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Example settings – embed all resources into a single HTML file
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    // Save raster images as PNG wrapped in SVG (cross‑browser friendly)
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // HTML conversion uses GDI+ and is Windows‑only.
                // Wrap the save call in a try‑catch to handle non‑Windows platforms gracefully.
                try
                {
                    // Save the document as HTML (lifecycle: save)
                    pdfDoc.Save(htmlPath, htmlOpts);
                    Console.WriteLine($"HTML file created at '{htmlPath}'.");
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