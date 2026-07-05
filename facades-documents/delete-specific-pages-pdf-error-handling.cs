using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // for PdfException

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

        // Verify that the input file exists before invoking Delete
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – '{inputPath}'.");
            return;
        }

        // Perform the deletion using PdfFileEditor
        try
        {
            PdfFileEditor editor = new PdfFileEditor();
            // Delete throws if the operation fails; it does not return a value
            editor.Delete(inputPath, pagesToDelete, outputPath);
            Console.WriteLine($"Pages {string.Join(", ", pagesToDelete)} deleted successfully.");
            Console.WriteLine($"Result saved to '{outputPath}'.");
        }
        catch (PdfException ex)
        {
            // Thrown for PDF‑specific errors (e.g., corrupted or unsupported PDF)
            Console.Error.WriteLine($"PDF processing error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Catch any other unexpected errors
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
