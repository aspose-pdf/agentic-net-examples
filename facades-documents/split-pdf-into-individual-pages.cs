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
        const string outputFolder = "SplitPages";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Template for output files; %NUM% will be replaced with the page number
        string fileNameTemplate = Path.Combine(outputFolder, "page%NUM%.pdf");

        try
        {
            // PdfFileEditor does NOT implement IDisposable; instantiate directly
            PdfFileEditor pdfEditor = new PdfFileEditor();

            // Split the PDF into single‑page PDFs using the template
            pdfEditor.SplitToPages(inputPath, fileNameTemplate);

            Console.WriteLine($"PDF successfully split into individual pages in '{outputFolder}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF split: {ex.Message}");
        }
    }
}