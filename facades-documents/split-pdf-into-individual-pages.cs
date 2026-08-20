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
        const string outputFolder = "output_pages";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Template for output files – %NUM% will be replaced by the page number (1‑based)
        string fileNameTemplate = Path.Combine(outputFolder, "page%NUM%.pdf");

        try
        {
            // PdfFileEditor provides the SplitToPages method that creates one PDF per page
            PdfFileEditor pdfEditor = new PdfFileEditor();
            pdfEditor.SplitToPages(inputPdfPath, fileNameTemplate);

            Console.WriteLine($"PDF split into individual pages successfully. Files are located in '{outputFolder}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during split operation: {ex.Message}");
        }
    }
}