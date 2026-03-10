using System;
using System.IO;
using Aspose.Pdf; // SvgSaveOptions and Document are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputSvgPath = "output.svg";

        // Verify the source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF, convert and save as SVG
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Initialize SVG save options (default constructor is sufficient for basic conversion)
            SvgSaveOptions svgOptions = new SvgSaveOptions();

            // Save the document as SVG using the options
            pdfDocument.Save(outputSvgPath, svgOptions);
        }

        Console.WriteLine($"PDF successfully converted to SVG: {outputSvgPath}");
    }
}