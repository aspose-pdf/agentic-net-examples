using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";
        // Output HTML file path (single HTML file containing all pages)
        const string outputHtml = "output.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure HTML save options
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                // Embed all resources (images, CSS, fonts) into the HTML file
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                // Save fonts as WOFF to preserve original typography
                FontSavingMode = HtmlSaveOptions.FontSavingModes.AlwaysSaveAsWOFF,
                // Keep the default behavior of not splitting into multiple pages
                // (single HTML file will contain the whole document)
            };

            // Save the PDF as a single-page HTML file
            pdfDoc.Save(outputHtml, htmlOpts);
        }

        Console.WriteLine($"Conversion completed: {outputHtml}");
    }
}