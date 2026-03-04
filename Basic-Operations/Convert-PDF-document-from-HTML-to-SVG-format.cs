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
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Initialize SVG save options
                SvgSaveOptions svgOptions = new SvgSaveOptions();

                // Save as SVG using the options (required for non‑PDF output)
                pdfDoc.Save(outputSvg, svgOptions);
            }

            Console.WriteLine($"PDF successfully converted to SVG: '{outputSvg}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}