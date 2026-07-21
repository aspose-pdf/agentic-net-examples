using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputHtml = "output.html";

        // Verify the source PDF exists.
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure HTML save options.
            HtmlSaveOptions saveOptions = new HtmlSaveOptions();

            // Embed all external resources (CSS, fonts, images) directly into the HTML file.
            saveOptions.PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml;

            // Embed raster images as Base64 strings inside SVG wrappers.
            saveOptions.RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg;

            // Optional: set a title for the generated HTML page.
            saveOptions.Title = Path.GetFileNameWithoutExtension(inputPdf);

            // Save the PDF as HTML using the configured options.
            pdfDoc.Save(outputHtml, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to HTML with embedded images: {outputHtml}");
    }
}