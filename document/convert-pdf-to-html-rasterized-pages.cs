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

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure HTML conversion options to rasterize each PDF page as an image
            // - Use RasterImagesSavingMode to force page rasterization
            // - Set ImageResolution (DPI) to 150
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg,
                ImageResolution = 150
            };

            // Save the document as HTML using the configured options
            pdfDoc.Save(outputHtml, htmlOpts);
        }

        Console.WriteLine($"PDF converted to HTML with rasterized pages: {outputHtml}");
    }
}
