using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";
        // Output HTML file path
        const string outputHtml = "output.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure HTML save options to rasterize each PDF page as an image
            // and set the raster image resolution to 150 DPI.
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                // Rasterize pages as external PNG files referenced via SVG.
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsExternalPngFilesReferencedViaSvg,
                // Set the DPI (resolution) for the generated raster images.
                ImageResolution = 150
            };

            // Save the document as HTML using the configured options
            pdfDoc.Save(outputHtml, htmlOpts);
        }

        Console.WriteLine($"PDF converted to HTML with rasterized pages: '{outputHtml}'");
    }
}
