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
        // Pages to delete (example: pages 2 and 3)
        int[] pagesToDelete = new int[] { 2, 3 };

        // Verify that the input file exists before invoking Delete
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file '{inputPath}' not found.");
            return;
        }

        try
        {
            // PdfFileEditor does not implement IDisposable, so no using block is needed
            PdfFileEditor editor = new PdfFileEditor();

            // Delete specified pages using the file‑path overload
            editor.Delete(inputPath, pagesToDelete, outputPath);

            Console.WriteLine($"Pages deleted successfully. Result saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Catch any runtime errors from the Delete operation
            Console.Error.WriteLine($"Error during Delete operation: {ex.Message}");
        }
    }
}