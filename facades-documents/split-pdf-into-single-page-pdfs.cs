using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Output folder where individual page PDFs will be saved
        const string outputFolder = "SplitPages";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Template for output files – %NUM% will be replaced by the page number
        string fileNameTemplate = Path.Combine(outputFolder, "page%NUM%.pdf");

        try
        {
            // PdfFileEditor does NOT implement IDisposable; do NOT wrap in using
            PdfFileEditor editor = new PdfFileEditor();

            // Split the PDF into single‑page documents and save them using the template
            editor.SplitToPages(inputPdf, fileNameTemplate);

            Console.WriteLine($"PDF split into individual pages under '{outputFolder}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during split operation: {ex.Message}");
        }
    }
}