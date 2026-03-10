using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputHtmlPath = "output.html";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Configure HTML save options – required when saving to a non‑PDF format
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    // Embed all generated resources (images, CSS, fonts) into the HTML file
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml
                };

                // HTML conversion uses GDI+ and is Windows‑only; handle the platform limitation gracefully
                try
                {
                    pdfDocument.Save(outputHtmlPath, htmlOptions);
                    Console.WriteLine($"PDF successfully converted to HTML: '{outputHtmlPath}'");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}