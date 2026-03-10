using System;
using System.IO;
using Aspose.Pdf; // Core PDF API – HtmlSaveOptions resides directly in this namespace

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";   // source PDF containing transparent (OCR) text
        const string outputHtml = "output.html"; // destination HTML file

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure HTML conversion options
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                // Preserve transparent OCR text so it becomes selectable in the resulting HTML
                SaveTransparentTexts = true,

                // Also preserve shadowed texts (texts hidden behind images) as transparent selectable text
                SaveShadowedTextsAsTransparentTexts = true,

                // Optional: embed all resources (images, CSS) into the single HTML file
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml
            };

            // Save the PDF as HTML using the configured options
            pdfDoc.Save(outputHtml, htmlOpts);
        }

        Console.WriteLine($"PDF converted to HTML with transparent text saved at '{outputHtml}'.");
    }
}
