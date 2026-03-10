using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, HtmlSaveOptions, etc.)

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath  = "input.pdf";
        // Desired output HTML file path
        const string outputHtmlPath = "output.html";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: load)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Initialize HTML save options (required for non‑PDF output)
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    // Example setting: embed raster images as PNG inside SVG wrappers
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                    // Optional: generate a single HTML file (default)
                    SplitIntoPages = false
                };

                // Save the document as HTML (lifecycle: save)
                // On non‑Windows platforms this may throw TypeInitializationException because GDI+ is unavailable.
                try
                {
                    pdfDoc.Save(outputHtmlPath, htmlOptions);
                    Console.WriteLine($"PDF successfully converted to HTML: {outputHtmlPath}");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires GDI+ and is only supported on Windows platforms.");
                }
            }
        }
        catch (Exception ex)
        {
            // General error handling
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}