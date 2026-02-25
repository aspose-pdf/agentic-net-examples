using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputHtml = "output.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure HTML conversion options (required for non‑PDF output)
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // HTML conversion relies on GDI+; wrap in try‑catch for cross‑platform safety
                try
                {
                    pdfDoc.Save(outputHtml, htmlOpts);
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