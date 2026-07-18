using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file to be split
        const string inputPdf = "input.pdf";

        // Directory where individual page PDFs will be saved
        const string outputDir = "SplitPages";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Template for the output files.
        // %NUM% will be replaced with the page number (1‑based) by PdfFileEditor.
        string fileNameTemplate = Path.Combine(outputDir, "page%NUM%.pdf");

        // Verify the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Use PdfFileEditor from Aspose.Pdf.Facades to split the PDF.
        // SplitToPages(string, string) writes each page to a separate file
        // according to the provided template.
        PdfFileEditor pdfEditor = new PdfFileEditor();
        pdfEditor.SplitToPages(inputPdf, fileNameTemplate);

        Console.WriteLine($"PDF split into individual pages under '{outputDir}'.");
    }
}