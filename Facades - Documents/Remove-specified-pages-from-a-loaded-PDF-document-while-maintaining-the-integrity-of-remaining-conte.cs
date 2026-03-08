using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the resulting PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Page numbers to delete (1‑based indexing as required by PdfFileEditor)
        int[] pagesToDelete = new int[] { 2, 3 };

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor does not implement IDisposable, so we instantiate it directly
            PdfFileEditor editor = new PdfFileEditor();

            // Delete the specified pages and write the result to outputPath
            bool success = editor.Delete(inputPath, pagesToDelete, outputPath);

            if (success)
                Console.WriteLine($"Pages {string.Join(", ", pagesToDelete)} removed successfully. Output saved to '{outputPath}'.");
            else
                Console.WriteLine("Page deletion failed.");
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}