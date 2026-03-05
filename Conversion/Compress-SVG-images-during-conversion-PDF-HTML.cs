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

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create HTML save options and enable SVG compression
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions();
            htmlOpts.CompressSvgGraphicsIfAny = true; // compress found SVG graphics to SVGZ

            // Optional: embed all resources into the single HTML file
            htmlOpts.PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml;

            // Optional: handle raster images as embedded PNGs inside wrapper SVGs
            htmlOpts.RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg;

            // Save the document as HTML using the configured options (lifecycle rule: use Save with options)
            pdfDoc.Save(outputHtml, htmlOpts);
        }

        Console.WriteLine($"Conversion completed. HTML saved to '{outputHtml}'.");
    }
}