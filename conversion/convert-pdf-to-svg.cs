using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Output SVG file path (or directory if multiple pages)
        const string outputSvg = "output.svg";

        // Verify the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Create default SVG save options
            SvgSaveOptions svgOptions = new SvgSaveOptions();

            // Optional: compress all pages into a single ZIP archive
            // svgOptions.CompressOutputToZipArchive = true;

            // Save each page as an SVG file (multiple files will be created if the PDF has more than one page)
            pdfDocument.Save(outputSvg, svgOptions);
        }

        Console.WriteLine($"PDF has been converted to SVG: {outputSvg}");
    }
}