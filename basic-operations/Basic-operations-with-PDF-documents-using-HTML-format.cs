using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the directory containing the PDF file.
        const string dataDir = "YOUR_DATA_DIRECTORY";

        // Input PDF and output HTML file paths.
        string pdfFile  = Path.Combine(dataDir, "PDF-to-HTML.pdf");
        string htmlFile = Path.Combine(dataDir, "PDF-to-HTML.html");

        // Verify that the source PDF exists.
        if (!File.Exists(pdfFile))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfFile}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal.
            using (Document pdfDocument = new Document(pdfFile))
            {
                // Initialize HtmlSaveOptions – required when saving to a non‑PDF format.
                HtmlSaveOptions saveOptions = new HtmlSaveOptions
                {
                    // Embed all resources (CSS, images, fonts) directly into the HTML file.
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,

                    // Save raster images as PNG data embedded within SVG wrappers.
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                try
                {
                    // Save the document as HTML using the specified options.
                    pdfDocument.Save(htmlFile, saveOptions);
                    Console.WriteLine($"HTML file saved to '{htmlFile}'.");
                }
                catch (TypeInitializationException)
                {
                    // HTML conversion relies on GDI+ and is Windows‑only.
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            // General error handling.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}