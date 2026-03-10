using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Use PdfPageEditor facade to modify PDF pages
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF file
            editor.BindPdf(inputPdf);

            // Example modifications:
            // Rotate all pages by 90 degrees
            editor.Rotation = 90;
            // Zoom pages to 80%
            editor.Zoom = 0.8f;
            // Apply the changes to the underlying document
            editor.ApplyChanges();

            // Prepare HTML save options
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                // Embed all resources (images, CSS, fonts) into a single HTML file
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                // Generate full HTML markup
                HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteAllHtml
            };

            // Export the modified document as HTML
            editor.Document.Save(outputHtml, htmlOpts);
        }

        Console.WriteLine($"Modified PDF saved as HTML to '{outputHtml}'.");
    }
}