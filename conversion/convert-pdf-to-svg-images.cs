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
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        using (Document pdfDocument = new Document(inputPdf))
        {
            // Save the whole document to SVG. Multiple pages will be saved as
            // output.svg, output_2.svg, output_3.svg, etc.
            pdfDocument.Save(outputSvg, new SvgSaveOptions());
        }

        Console.WriteLine($"PDF has been converted to SVG images at '{outputSvg}'.");
    }
}
