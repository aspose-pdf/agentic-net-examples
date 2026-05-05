using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF file.
        const string inputPath = "input.pdf";

        // Directory where individual page PDFs will be saved.
        const string outputDir = "output_pages";

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);

        // Template for the output files. %NUM% will be replaced with the page number.
        string fileNameTemplate = Path.Combine(outputDir, "page_%NUM%.pdf");

        // Verify that the source file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does not implement IDisposable, so we instantiate it directly.
        PdfFileEditor editor = new PdfFileEditor();

        // Split the PDF into single‑page documents and save them using the template.
        editor.SplitToPages(inputPath, fileNameTemplate);

        Console.WriteLine("PDF has been split into individual pages.");
    }
}