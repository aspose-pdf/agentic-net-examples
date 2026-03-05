using System;
using System.IO;
using Aspose.Pdf; // Core PDF API (HtmlSaveOptions resides here)

// Render transparent (OCR‑extracted) text when converting PDF to HTML
class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";   // source PDF
        const string outputHtml = "output.html"; // target HTML file

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use using for deterministic disposal)
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Initialise HTML save options
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Preserve transparent OCR text so it becomes selectable in the HTML output
                    SaveTransparentTexts = true,

                    // Preserve shadowed OCR text as transparent selectable text (optional but often desired)
                    SaveShadowedTextsAsTransparentTexts = true,

                    // Keep text as real text (do NOT render it as an image)
                    RenderTextAsImage = false,

                    // Example: embed all resources into a single HTML file
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml
                };

                // HTML conversion may require GDI+ (Windows only). Wrap in try‑catch to avoid crashes on non‑Windows platforms.
                try
                {
                    pdfDoc.Save(outputHtml, htmlOpts); // lifecycle rule: use Save(string, SaveOptions)
                    Console.WriteLine($"HTML saved to '{outputHtml}'.");
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
