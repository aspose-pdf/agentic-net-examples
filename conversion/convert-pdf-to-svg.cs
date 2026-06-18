using System;
using System.IO;
using Aspose.Pdf;   // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";
        // Output SVG file path (multiple pages will be saved as output.svg, output_2.svg, etc.)
        const string outputSvg = "output.svg";

        // Verify that the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Create default SVG save options (no custom settings required)
            SvgSaveOptions svgOptions = new SvgSaveOptions();

            // Save the PDF as SVG; each page will be written to a separate SVG file
            pdfDocument.Save(outputSvg, svgOptions);
        }

        Console.WriteLine($"PDF successfully converted to SVG: {outputSvg}");
    }
}