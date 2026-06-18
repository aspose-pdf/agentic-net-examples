using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the filled PDF that needs to be split
        const string inputPdf = "filled.pdf";

        // Template for the output single‑page PDFs.
        // %NUM% will be replaced with the page number (1‑based).
        const string outputTemplate = "output/page_%NUM%.pdf";

        // Verify that the source file exists
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

        // Split the PDF into single‑page documents and save them using the template
        // This method creates one PDF per page, naming them according to the template.
        editor.SplitToPages(inputPdf, outputTemplate);

        Console.WriteLine("PDF successfully split into individual pages.");
    }
}