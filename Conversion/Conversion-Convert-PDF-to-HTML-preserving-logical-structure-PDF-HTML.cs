using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf;               // HtmlSaveOptions resides in this namespace (no Aspose.Pdf.Saving)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputHtml = "output.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure HTML conversion options
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                // Embed all resources (images, CSS) into the single HTML file
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                // Preserve the original layout of raster images
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                // Use the default markup generation mode (writes full HTML)
                HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteAllHtml
            };

            // Save the PDF as HTML while preserving any existing logical structure (tags)
            pdfDoc.Save(outputHtml, htmlOpts);
        }

        Console.WriteLine($"PDF successfully converted to HTML: '{outputHtml}'");
    }
}