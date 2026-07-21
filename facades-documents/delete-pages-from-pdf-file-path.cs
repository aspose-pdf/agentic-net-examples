using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF file path after pages 2‑5 are removed
        const string outputPath = "output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Pages to delete (2 through 5). PdfFileEditor uses 1‑based page numbers.
        int[] pagesToDelete = new int[] { 2, 3, 4, 5 };

        // PdfFileEditor does NOT implement IDisposable, so do NOT use a using block.
        PdfFileEditor editor = new PdfFileEditor();

        // Delete the specified pages and save the result to the output file.
        bool success = editor.Delete(inputPath, pagesToDelete, outputPath);

        if (success)
        {
            Console.WriteLine($"Pages 2‑5 deleted successfully. Output saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to delete pages.");
        }
    }
}