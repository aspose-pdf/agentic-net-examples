using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Page number to delete (1‑based indexing)
        const int pageToDelete = 2;
        // Output PDF file path
        const string outputPath = "output.pdf";

        // Verify that the input file exists before invoking Delete
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – '{inputPath}'.");
            return;
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable; instantiate directly
            PdfFileEditor editor = new PdfFileEditor();

            // Delete overload expects an array of page numbers
            int[] pagesToDelete = new[] { pageToDelete };

            // Delete the specified page(s) and write the result to outputPath
            editor.Delete(inputPath, pagesToDelete, outputPath);

            Console.WriteLine($"Page {pageToDelete} deleted successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors (e.g., invalid page number, I/O issues)
            Console.Error.WriteLine($"Error during Delete operation: {ex.Message}");
        }
    }
}
