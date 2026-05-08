using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the filled PDF document
        const string inputPdf = "filled.pdf";

        // Template for output files – %NUM% will be replaced with the page number
        const string outputTemplate = "output/page%NUM%.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(outputTemplate);
        if (!string.IsNullOrEmpty(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // PdfFileEditor does not implement IDisposable, so we instantiate it directly
        PdfFileEditor editor = new PdfFileEditor();

        // Split the PDF into single‑page files using the template
        editor.SplitToPages(inputPdf, outputTemplate);

        Console.WriteLine("PDF split into individual pages successfully.");
    }
}