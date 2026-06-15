using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputHtml = "output.html";

        // Verify source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Configure HTML conversion options
            HtmlSaveOptions saveOptions = new HtmlSaveOptions();

            // Render each page background as a single PNG image (embedded per page)
            saveOptions.RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsEmbeddedPartsOfPngPageBackground;

            // Optional: embed all resources (CSS, images, etc.) into the HTML file
            saveOptions.PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml;

            // Save the document as HTML using the configured options
            pdfDocument.Save(outputHtml, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to HTML: {outputHtml}");
    }
}