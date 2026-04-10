using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "filled.pdf";
        // %NUM% will be replaced with the page number (1‑based)
        const string outputTemplate = "output/page%NUM%.pdf";

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

        // PdfFileEditor does NOT implement IDisposable, so do NOT use a using block
        PdfFileEditor editor = new PdfFileEditor();

        // Split the PDF into single‑page files using the template path
        editor.SplitToPages(inputPdf, outputTemplate);

        Console.WriteLine("PDF split into individual pages successfully.");
    }
}