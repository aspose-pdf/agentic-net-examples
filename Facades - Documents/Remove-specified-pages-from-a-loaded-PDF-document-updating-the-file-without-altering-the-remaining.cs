using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";

        // Output PDF file path (will contain the remaining pages)
        const string outputPath = "output.pdf";

        // Pages to remove – 1‑based page numbers as required by Aspose.Pdf
        int[] pagesToDelete = new int[] { 2, 4, 5 };

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor performs the delete operation without needing a Document instance.
            PdfFileEditor editor = new PdfFileEditor();

            // Delete the specified pages and save the result to a new file.
            // The method returns true if the operation succeeded.
            bool success = editor.Delete(inputPath, pagesToDelete, outputPath);

            if (success)
                Console.WriteLine($"Pages deleted successfully. Result saved to '{outputPath}'.");
            else
                Console.Error.WriteLine("Failed to delete pages.");
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors (e.g., file access issues)
            Console.Error.WriteLine($"Exception: {ex.Message}");
        }
    }
}