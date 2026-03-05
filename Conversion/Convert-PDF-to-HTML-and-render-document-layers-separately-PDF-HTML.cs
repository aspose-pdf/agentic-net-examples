using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputHtml = "output.html";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use using for deterministic disposal)
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure HTML conversion options
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Create a separate HTML <div> for each PDF marked‑content layer
                    ConvertMarkedContentToLayers = true,

                    // Example: embed raster images as PNG inside SVG wrappers
                    // RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,

                    // Example: keep all pages in a single HTML file
                    // SplitIntoPages = false
                };

                // HTML conversion may require GDI+ (Windows only). Wrap in try‑catch to handle other platforms.
                try
                {
                    pdfDoc.Save(outputHtml, htmlOpts);
                    Console.WriteLine($"HTML saved to '{outputHtml}' with layers.");
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