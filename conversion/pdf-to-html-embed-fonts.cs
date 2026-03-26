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

        using (Document doc = new Document(inputPdf))
        {
            HtmlSaveOptions options = new HtmlSaveOptions
            {
                // Embed all resources (including fonts) directly into the HTML as base64 data URIs
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                // Save referenced fonts as WOFF and generate @font-face rules in the CSS
                FontSavingMode = HtmlSaveOptions.FontSavingModes.AlwaysSaveAsWOFF
            };

            try
            {
                doc.Save(outputHtml, options);
                Console.WriteLine($"HTML with embedded fonts saved to '{outputHtml}'.");
            }
            catch (TypeInitializationException)
            {
                // HTML conversion relies on GDI+ and is Windows‑only
                Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped.");
            }
        }
    }
}