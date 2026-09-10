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

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Pages to delete: 2, 3, 4, 5 (1‑based indexing)
        int[] pagesToDelete = new int[] { 2, 3, 4, 5 };

        // PdfFileEditor does NOT implement IDisposable, so no using block is needed
        PdfFileEditor editor = new PdfFileEditor();

        // Perform the deletion; Delete returns true on success
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