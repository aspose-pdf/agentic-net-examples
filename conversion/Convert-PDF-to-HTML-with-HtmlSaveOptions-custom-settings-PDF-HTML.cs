using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputHtml = "output.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure HtmlSaveOptions with custom settings
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Embed raster images as PNG inside SVG wrappers
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                    // Generate a single HTML file (not one per page)
                    SplitIntoPages = false,
                    // Use flow layout (default) – can be omitted
                    FixedLayout = false,
                    // Set the HTML page title
                    Title = Path.GetFileNameWithoutExtension(outputHtml)
                };

                // Save the PDF as HTML using the custom options
                pdfDoc.Save(outputHtml, htmlOpts);
                Console.WriteLine($"PDF successfully converted to HTML: {outputHtml}");
            }
        }
        catch (TypeInitializationException)
        {
            // HTML conversion requires GDI+ on Windows; handle non‑Windows platforms gracefully
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipping on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}