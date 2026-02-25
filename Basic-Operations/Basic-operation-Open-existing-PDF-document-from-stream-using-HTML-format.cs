using System;
using System.IO;
using Aspose.Pdf;               // Document, HtmlSaveOptions, etc.

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputHtmlPath = "output.html";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Open the PDF from a file stream.
        using (FileStream pdfStream = File.OpenRead(inputPdfPath))
        // Document implements IDisposable – wrap it in a using block.
        using (Document pdfDoc = new Document(pdfStream))
        {
            // HTML conversion requires GDI+ (Windows only). Wrap in try‑catch for cross‑platform safety.
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                // Example settings – adjust as needed.
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };

            try
            {
                pdfDoc.Save(outputHtmlPath, htmlOpts);   // Save as HTML using explicit options.
                Console.WriteLine($"HTML saved to '{outputHtmlPath}'.");
            }
            catch (TypeInitializationException)
            {
                Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error during HTML save: {ex.Message}");
            }
        }
    }
}