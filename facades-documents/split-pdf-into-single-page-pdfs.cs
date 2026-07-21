using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output folder where each page PDF will be saved
        const string outputFolder = "SplitPages";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // File name template – %NUM% will be replaced with the page number (1‑based)
        string fileNameTemplate = Path.Combine(outputFolder, "page%NUM%.pdf");

        // Use PdfFileEditor (facade) to split the PDF into single‑page documents
        PdfFileEditor pdfEditor = new PdfFileEditor();
        pdfEditor.SplitToPages(inputPdfPath, fileNameTemplate);

        Console.WriteLine($"PDF has been split into individual pages in folder: {outputFolder}");
    }
}