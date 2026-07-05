using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "filled.pdf";
        const string outputTemplate = "output/page%NUM%.pdf";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(outputTemplate);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Split the PDF into single‑page files using the template
        PdfFileEditor pdfEditor = new PdfFileEditor();
        pdfEditor.SplitToPages(inputPdf, outputTemplate);

        Console.WriteLine("PDF split into individual pages successfully.");
    }
}