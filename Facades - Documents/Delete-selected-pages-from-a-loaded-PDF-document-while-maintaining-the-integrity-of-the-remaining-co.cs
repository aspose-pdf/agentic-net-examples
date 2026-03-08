using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the resulting PDF after deletion
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Specify the page numbers to delete (1‑based indexing)
        int[] pagesToDelete = new int[] { 2, 3 };

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor does not implement IDisposable, so no using block is needed
            PdfFileEditor editor = new PdfFileEditor();

            // Delete the specified pages and save the result to a new file
            bool success = editor.Delete(inputPath, pagesToDelete, outputPath);

            Console.WriteLine(success
                ? $"Selected pages deleted successfully. Output saved to '{outputPath}'."
                : "Page deletion failed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during page deletion: {ex.Message}");
        }
    }
}