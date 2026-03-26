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

        using (Document pdfDoc = new Document(inputPdf))
        {
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                // Embed all CSS, images and fonts into a single HTML file
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                // Ensure the whole document is written to one HTML page
                SplitIntoPages = false
            };

            pdfDoc.Save(outputHtml, htmlOpts);
        }

        Console.WriteLine($"PDF converted to HTML: {outputHtml}");
    }
}
