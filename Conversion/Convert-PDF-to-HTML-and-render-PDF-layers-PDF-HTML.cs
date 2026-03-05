using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";
        // Output HTML file path
        const string outputHtmlPath = "output.html";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: create & load)
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Configure HTML conversion options
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    // Render PDF marked‑content layers as separate HTML divs
                    ConvertMarkedContentToLayers = true,
                    // Example: embed raster images as PNG inside SVG wrappers
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save the document as HTML using the specified options (lifecycle: save)
                pdfDocument.Save(outputHtmlPath, htmlOptions);
            }

            Console.WriteLine($"PDF successfully converted to HTML with layers: {outputHtmlPath}");
        }
        // HTML conversion relies on GDI+ and may fail on non‑Windows platforms
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion error: {ex.Message}");
        }
    }
}