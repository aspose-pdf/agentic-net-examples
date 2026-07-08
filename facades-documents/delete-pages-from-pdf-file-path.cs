using System;
using System.IO;
using System.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF file path (pages 2‑5 will be removed)
        const string outputPath = "output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Pages to delete: 2, 3, 4, 5 (1‑based indexing)
        int[] pagesToDelete = Enumerable.Range(2, 4).ToArray();

        // PdfFileEditor does not implement IDisposable, so no using block is required
        PdfFileEditor editor = new PdfFileEditor();

        // Delete the specified pages and save the result to the output file
        bool success = editor.Delete(inputPath, pagesToDelete, outputPath);

        if (success)
            Console.WriteLine($"Pages 2‑5 deleted successfully. Output saved to '{outputPath}'.");
        else
            Console.Error.WriteLine("Failed to delete pages.");
    }
}