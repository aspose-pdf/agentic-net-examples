using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";

        // Output folder where each page PDF will be saved
        const string outputFolder = "Pages";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Template for output files; %NUM% will be replaced by the page number
        string fileNameTemplate = Path.Combine(outputFolder, "page%NUM%.pdf");

        // PdfFileEditor does not implement IDisposable, so we instantiate it directly
        PdfFileEditor pdfEditor = new PdfFileEditor();

        // Split the PDF into single‑page documents and save them using the template
        pdfEditor.SplitToPages(inputPath, fileNameTemplate);

        Console.WriteLine($"PDF has been split into individual pages in folder: {outputFolder}");
    }
}