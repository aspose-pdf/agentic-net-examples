using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputHtml = "output.html";
        const string svgFolder = "svg_images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the folder for SVG images exists
        Directory.CreateDirectory(svgFolder);

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure HTML save options
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                // Store only SVG images in the specified folder
                SpecialFolderForSvgImages = svgFolder,
                // Example raster image handling (optional)
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };

            // Save the document as HTML using explicit save options
            pdfDoc.Save(outputHtml, htmlOpts);
        }

        Console.WriteLine($"HTML saved to '{outputHtml}'. SVG images stored in '{svgFolder}'.");
    }
}