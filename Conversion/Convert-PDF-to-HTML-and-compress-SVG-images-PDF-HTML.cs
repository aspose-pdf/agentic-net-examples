using System;
using System.IO;
using Aspose.Pdf;

class PdfToHtmlConverter
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputHtmlPath = "output.html";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use Document constructor)
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Configure HTML conversion options
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    // Compress any extracted SVG graphics into SVGZ format
                    CompressSvgGraphicsIfAny = true,

                    // Optional: embed raster images inside SVG wrappers (helps keep SVG graphics compact)
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save the document as HTML with the specified options (lifecycle rule: use Save with options)
                pdfDocument.Save(outputHtmlPath, htmlOptions);
            }

            Console.WriteLine($"PDF successfully converted to HTML: {outputHtmlPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}