using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.svg"; // first page SVG; subsequent pages get suffixes automatically

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPath))
        {
            // Use SvgSaveOptions (the modern API) to convert each page to SVG.
            // When a file name without a page placeholder is supplied, Aspose.PDF creates
            // separate SVG files for each page, appending a numeric suffix (e.g., output_1.svg, output_2.svg, ...).
            SvgSaveOptions svgOptions = new SvgSaveOptions();
            pdfDoc.Save(outputPath, svgOptions);
        }

        Console.WriteLine($"PDF converted to SVG(s) at '{outputPath}'");
    }
}
