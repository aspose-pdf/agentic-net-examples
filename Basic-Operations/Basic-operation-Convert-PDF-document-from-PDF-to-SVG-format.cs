using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputSvg = "output.svg";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Initialize SVG save options (required to ensure non‑PDF output)
            SvgSaveOptions svgOptions = new SvgSaveOptions();

            // Convert and save the PDF as an SVG file
            pdfDoc.Save(outputSvg, svgOptions);
        }

        Console.WriteLine($"SVG file saved to '{outputSvg}'.");
    }
}