using System;
using System.IO;
using Aspose.Pdf;   // All SaveOptions (including SvgSaveOptions) are in this namespace

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";
        // Desired output SVG file path
        const string outputSvg = "output.svg";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Wrap the Document in a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Initialise SVG save options (required to actually produce SVG)
            SvgSaveOptions svgOptions = new SvgSaveOptions();

            // Save the PDF as SVG using the explicit options
            pdfDoc.Save(outputSvg, svgOptions);
        }

        Console.WriteLine($"PDF successfully converted to SVG: {outputSvg}");
    }
}