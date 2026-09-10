using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output SVG file path (first page will be saved as this name,
        // subsequent pages will be saved as input_2.svg, input_3.svg, etc.)
        const string outputSvgPath = "output.svg";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Create default SVG save options
            SvgSaveOptions svgOptions = new SvgSaveOptions();

            // Save the document as SVG. The Save method with a SaveOptions
            // instance writes SVG files according to the options.
            pdfDocument.Save(outputSvgPath, svgOptions);
        }

        Console.WriteLine($"PDF has been converted to SVG: {outputSvgPath}");
    }
}