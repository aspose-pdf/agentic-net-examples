using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, HtmlSaveOptions, etc.)

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Output HTML file path
        const string htmlPath = "output.html";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use Document constructor inside a using block)
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Configure HTML conversion options
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Render each PDF marked‑content layer as a separate HTML <div>
                    ConvertMarkedContentToLayers = true,

                    // Optional: embed all resources into the single HTML file
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,

                    // Optional: improve image handling (choose one of the enum values as needed)
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save the document as HTML using the explicit HtmlSaveOptions
                // (save-to-non-pdf-always-use-save-options rule)
                try
                {
                    pdfDoc.Save(htmlPath, htmlOpts);
                    Console.WriteLine($"PDF successfully converted to HTML with layers: '{htmlPath}'");
                }
                catch (TypeInitializationException)
                {
                    // HTML conversion relies on GDI+ and is Windows‑only.
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipping on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}