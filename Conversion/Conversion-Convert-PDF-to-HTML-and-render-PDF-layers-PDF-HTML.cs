using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API and all SaveOptions subclasses
using Aspose.Pdf.Text;                // Required for text-related options (if needed)

class PdfToHtmlConverter
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputHtmlPath = "output.html";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure HTML conversion options.
                // HtmlSaveOptions must be passed explicitly; otherwise a PDF is written.
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Embed all resources (images, CSS, fonts) into the single HTML file.
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,

                    // Save raster images as PNG wrapped in SVG (preserves transparency and layers).
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,

                    // Optional: generate a single HTML file (no page splitting).
                    SplitIntoPages = false
                };

                // HTML conversion on non‑Windows platforms requires GDI+.
                // Wrap the save call in a try‑catch to handle possible TypeInitializationException.
                try
                {
                    pdfDoc.Save(outputHtmlPath, htmlOpts);
                    Console.WriteLine($"PDF successfully converted to HTML: {outputHtmlPath}");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipping on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}