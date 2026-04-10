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
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure HTML conversion options
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions();

                // Embed raster images as Base64‑encoded PNGs inside SVG wrappers
                htmlOpts.RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg;

                // Embed all referenced resources (CSS, fonts, images) into the single HTML file
                htmlOpts.PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml;

                // Save the document as HTML with the specified options
                pdfDoc.Save(outputHtml, htmlOpts);
            }

            Console.WriteLine($"PDF successfully converted to HTML with embedded images: '{outputHtml}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}