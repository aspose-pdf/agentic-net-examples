using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF file path (pages from startPage to the end)
        const string outputPath = "output_split.pdf";
        // Page number to start splitting (1‑based indexing)
        const int startPage = 3;

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable; instantiate directly
            PdfFileEditor editor = new PdfFileEditor();

            // Split from the specified start page to the end of the document
            // SplitToEnd saves the rear part (from startPage onward) as a new PDF file
            bool success = editor.SplitToEnd(inputPath, startPage, outputPath);

            if (success)
                Console.WriteLine($"PDF successfully split. Output saved to '{outputPath}'.");
            else
                Console.Error.WriteLine("PDF split operation failed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during split operation: {ex.Message}");
        }
    }
}