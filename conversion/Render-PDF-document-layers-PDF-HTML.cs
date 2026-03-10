using System;
using System.IO;
using Aspose.Pdf; // All save options, including HtmlSaveOptions, are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputHtml = "output.html";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use Document constructor inside a using block)
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure HTML conversion options
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Enable conversion of PDF marked‑content (layers) to HTML <div data-pdflayer="...">
                    ConvertMarkedContentToLayers = true,

                    // Example: embed raster images as PNG inside SVG wrappers
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,

                    // Keep all pages in a single HTML file (optional)
                    SplitIntoPages = false
                };

                // HTML conversion uses GDI+ on Windows; wrap in try‑catch for cross‑platform safety
                try
                {
                    pdfDoc.Save(outputHtml, htmlOpts); // Save with explicit HtmlSaveOptions (rule: always pass options for non‑PDF formats)
                    Console.WriteLine($"HTML saved to '{outputHtml}'.");
                }
                catch (TypeInitializationException)
                {
                    // GDI+ not available on macOS/Linux
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