using System;
using System.IO;
using Aspose.Pdf;               // Core API
// No additional namespaces are required for SVG conversion

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";
        // Output SVG file path (first page). Additional pages will be saved as input_svg_2.svg, etc.
        const string outputSvg = "output.svg";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document and ensure deterministic disposal
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Initialize default SVG save options
            SvgSaveOptions svgOptions = new SvgSaveOptions();

            // Save the PDF as SVG; multiple pages will produce multiple SVG files
            pdfDocument.Save(outputSvg, svgOptions);
        }

        Console.WriteLine($"PDF has been converted to SVG images. Check the output files.");
    }
}