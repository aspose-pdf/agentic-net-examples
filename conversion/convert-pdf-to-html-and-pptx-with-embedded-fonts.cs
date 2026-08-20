using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF path
        const string pdfPath = "input.pdf";

        // Output HTML path
        const string htmlPath = "output.html";

        // Output PPTX path
        const string pptxPath = "output.pptx";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(pdfPath))
            {
                // ---------- Convert PDF to HTML ----------
                // HtmlSaveOptions must be passed explicitly; otherwise the output is always PDF.
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    // Example: embed fonts as WOFF to ensure they are available in the HTML.
                    FontSavingMode = HtmlSaveOptions.FontSavingModes.AlwaysSaveAsWOFF,

                    // Optional: embed all resources into a single HTML file.
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,

                    // Optional: handle raster images as embedded PNGs inside SVG wrappers.
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // HTML conversion uses GDI+ and is Windows‑only. Wrap in try‑catch for cross‑platform safety.
                try
                {
                    pdfDoc.Save(htmlPath, htmlOptions);
                    Console.WriteLine($"PDF successfully converted to HTML: {htmlPath}");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipping HTML export on this platform.");
                }

                // ---------- Convert PDF to PPTX with embedded fonts ----------
                // PptxSaveOptions controls PPTX export. Setting CacheGlyphs embeds font glyphs,
                // which provides uniform appearance regardless of the viewer's installed fonts.
                PptxSaveOptions pptxOptions = new PptxSaveOptions
                {
                    CacheGlyphs = true
                };

                pdfDoc.Save(pptxPath, pptxOptions);
                Console.WriteLine($"PDF successfully converted to PPTX with embedded fonts: {pptxPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
