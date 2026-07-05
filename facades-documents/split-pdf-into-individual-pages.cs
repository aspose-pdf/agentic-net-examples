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

        // Build the file name template.
        // %NUM% will be replaced by the page number (1‑based) by PdfFileEditor.
        // Example: C:\...\SplitPages\page%NUM%.pdf -> page1.pdf, page2.pdf, ...
        string fileNameTemplate = Path.Combine(outputFolder, "page%NUM%.pdf");

        try
        {
            // PdfFileEditor provides the SplitToPages method for this task.
            PdfFileEditor editor = new PdfFileEditor();

            // Split the PDF into individual page PDFs using the template.
            editor.SplitToPages(inputPdfPath, fileNameTemplate);

            Console.WriteLine($"PDF split into individual pages under '{outputFolder}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during split operation: {ex.Message}");
        }
    }
}