using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputSvg = "output.svg";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure SVG save options. The default behavior saves all pages into a single SVG.
                // ScaleToPixels enables CSS style embedding.
                var svgOptions = new SvgSaveOptions
                {
                    ScaleToPixels = true
                };

                // Save the extracted vector graphics as a multi‑page SVG document
                pdfDoc.Save(outputSvg, svgOptions);
            }

            Console.WriteLine($"Vector graphics extracted to multi-page SVG: {outputSvg}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
