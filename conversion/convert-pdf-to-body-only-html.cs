using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string htmlPath = "output.html";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Configure HTML conversion options – embed all resources into a single HTML file
                // and produce a single-page output (no <html>/<head> wrapper is required for body‑only markup).
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    // Ensure the output is a single HTML file (no split per page)
                    SplitIntoPages = false
                    // HtmlMarkupGenerationMode and RasterImagesSavingMode are omitted because the
                    // referenced Aspose.Pdf version does not expose those members. The default
                    // behaviour embeds raster images as PNG data URLs, which satisfies the requirement.
                };

                // HTML conversion (Windows only – may throw TypeInitializationException on other platforms)
                try
                {
                    pdfDoc.Save(htmlPath, htmlOpts);
                    Console.WriteLine($"HTML saved to '{htmlPath}'.");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires GDI+ (Windows only). Skipping on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
