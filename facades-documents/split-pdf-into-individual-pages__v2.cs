using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file to be split
        const string inputPdf = "input.pdf";

        // Template for output files – %NUM% will be replaced by the page number (1‑based)
        // Example: "output/page_%NUM%.pdf" will produce page_1.pdf, page_2.pdf, etc.
        const string outputTemplate = "output/page_%NUM%.pdf";

        // Verify that the input file exists
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

        // PdfFileEditor does NOT implement IDisposable, so we instantiate it directly
        PdfFileEditor editor = new PdfFileEditor();

        // Split the PDF into individual pages; each page is saved to a separate file
        // according to the template provided.
        editor.SplitToPages(inputPdf, outputTemplate);

        Console.WriteLine("PDF split into individual pages successfully.");
    }
}