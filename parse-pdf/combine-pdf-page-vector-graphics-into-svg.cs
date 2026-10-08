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
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure SVG save options. ScaleToPixels preserves vector graphics.
                var svgOpts = new SvgSaveOptions
                {
                    ScaleToPixels = true
                };

                // Save the extracted vector graphics as a single (multi‑page) SVG file.
                pdfDoc.Save(outputSvg, svgOpts);
            }

            Console.WriteLine($"Vector graphics extracted to '{outputSvg}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
