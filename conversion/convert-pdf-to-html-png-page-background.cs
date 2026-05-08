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
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure HTML conversion options
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                // Render all background images as a single PNG per page
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsEmbeddedPartsOfPngPageBackground,
                // Preserve page layout (optional but helps keep background rendering)
                FixedLayout = true
            };

            // Save the document as HTML with the specified options
            pdfDoc.Save(outputHtml, htmlOpts);
        }

        Console.WriteLine($"PDF successfully converted to HTML: {outputHtml}");
    }
}