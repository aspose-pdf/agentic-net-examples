using System;
using System.IO;
using Aspose.Pdf;                     // Core Aspose.Pdf namespace (contains Document, HtmlSaveOptions, etc.)

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
                // Initialize HtmlSaveOptions (required for non‑PDF output)
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions();

                // Optional: embed all resources (images, CSS, fonts) into the single HTML file
                htmlOptions.PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml;

                // Optional: handle raster images as PNGs wrapped in SVG (cross‑platform safe mode)
                htmlOptions.RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg;

                // Save the document as HTML (lifecycle: save)
                pdfDoc.Save(outputHtmlPath, htmlOptions);
                Console.WriteLine($"PDF successfully converted to HTML: {outputHtmlPath}");
            }
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+; on non‑Windows platforms this exception may be thrown.
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}