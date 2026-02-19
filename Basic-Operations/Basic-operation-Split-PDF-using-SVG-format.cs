using System;
using System.IO;
using Aspose.Pdf;

class SplitPdfToSvg
{
    static void Main(string[] args)
    {
        // Input PDF path (first argument) and output SVG base path (second argument)
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: SplitPdfToSvg <input-pdf> <output-svg-base>");
            return;
        }

        string inputPdfPath = args[0];
        string outputSvgPath = args[1];

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Configure SVG save options (SvgSaveOptions lives in Aspose.Pdf namespace)
            var svgOptions = new SvgSaveOptions
            {
                // When TreatTargetFileNameAsDirectory is true, a folder with the same name as the
                // output file (without extension) will be created and each page will be saved as
                // <output>.svg, <output>_2.svg, <output>_3.svg, etc.
                TreatTargetFileNameAsDirectory = true
            };

            // Save the document as SVG. The method will generate one SVG per page.
            pdfDocument.Save(outputSvgPath, svgOptions);

            Console.WriteLine($"PDF successfully split into SVG files at: {outputSvgPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}