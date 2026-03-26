using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.svg";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(inputPath))
        {
            // SvgSaveOptions controls SVG export. CSS style embedding is enabled by default.
            Aspose.Pdf.SvgSaveOptions saveOptions = new Aspose.Pdf.SvgSaveOptions();

            // Save the document as SVG using the options
            pdfDoc.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"PDF converted to SVG: {outputPath}");
    }
}