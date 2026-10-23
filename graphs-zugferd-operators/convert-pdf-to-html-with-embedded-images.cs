using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputHtmlPath = "output.html";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Configure HTML save options to embed all resources (including images) directly into the HTML file
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                // Embed all parts (CSS, fonts, images) into the single HTML output as Base64 data URIs
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                // Ensure the output is a single HTML file (no page splitting)
                SplitIntoPages = false
            };

            // HTML conversion relies on GDI+ and is Windows‑only; handle possible exceptions on other platforms
            try
            {
                pdfDoc.Save(outputHtmlPath, htmlOpts);
                Console.WriteLine($"PDF successfully converted to HTML with embedded images: {outputHtmlPath}");
            }
            catch (TypeInitializationException)
            {
                Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error during HTML conversion: {ex.Message}");
            }
        }
    }
}
