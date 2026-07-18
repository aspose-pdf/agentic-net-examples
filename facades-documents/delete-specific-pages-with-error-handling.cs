using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF file path after deletion
        const string outputPath = "output.pdf";
        // Pages to delete (1‑based indexing)
        int[] pagesToDelete = new int[] { 2, 3 };

        // Verify that the input file exists before invoking the facade
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – '{inputPath}'.");
            return;
        }

        // Create the PdfFileEditor instance (it does not implement IDisposable)
        PdfFileEditor editor = new PdfFileEditor();

        // TryDelete returns false if the operation fails; it does not throw.
        bool success = editor.TryDelete(inputPath, pagesToDelete, outputPath);

        if (success)
        {
            Console.WriteLine($"Pages {string.Join(", ", pagesToDelete)} deleted successfully.");
            Console.WriteLine($"Result saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to delete pages. Check the input file and page numbers.");
        }
    }
}