using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputHtml = "output.html";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure HTML conversion options
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions();

            // Render all background images as a single PNG per page
            htmlOpts.RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsEmbeddedPartsOfPngPageBackground;

            // Optional: improve rendering of tiled background images
            htmlOpts.TryMergeAdjacentSameBackgroundImages = true;

            // Save the document as HTML using the configured options
            pdfDoc.Save(outputHtml, htmlOpts);
        }

        Console.WriteLine($"PDF successfully converted to HTML with PNG backgrounds: {outputHtml}");
    }
}