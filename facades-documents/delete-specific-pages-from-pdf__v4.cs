using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Pages to delete – Aspose.Pdf uses 1‑based indexing
        int[] pagesToDelete = new int[] { 3, 6, 9 };

        // PdfFileEditor does NOT implement IDisposable; instantiate directly
        PdfFileEditor editor = new PdfFileEditor();

        try
        {
            // Delete the specified pages and write the result to outputPath
            // Correct overload: Delete(string inputFile, int[] pages, string outputFile)
            editor.Delete(inputPath, pagesToDelete, outputPath);
            Console.WriteLine($"Deleted pages {string.Join(", ", pagesToDelete)}. Saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during deletion: {ex.Message}");
        }
    }
}
