using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputSvgPath = "output.svg";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(inputPdfPath))
        {
            // Initialize SVG save options
            Aspose.Pdf.SvgSaveOptions svgOptions = new Aspose.Pdf.SvgSaveOptions();

            // CSS styles are embedded by default in the generated SVG.
            // If additional control is needed, properties such as
            // ScaleToPixels, CompressOutputToZipArchive, etc., can be set here.

            // Save the document as SVG
            pdfDocument.Save(outputSvgPath, svgOptions);
        }

        Console.WriteLine($"PDF successfully converted to SVG: {outputSvgPath}");
    }
}