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
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" not found.");
            return;
        }

        // Use PdfFileEditor to delete the specified pages.
        // TryDelete returns false instead of throwing if the operation fails.
        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.TryDelete(inputPath, pagesToDelete, outputPath);

        if (success)
        {
            Console.WriteLine($"Pages {string.Join(", ", pagesToDelete)} successfully deleted.");
            Console.WriteLine($"Result saved to \"{outputPath}\".");
        }
        else
        {
            Console.Error.WriteLine("Failed to delete pages. Check the input file and page numbers.");
        }
    }
}