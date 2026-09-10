using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file, pages to delete, and output PDF file
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        int[] pagesToDelete = new int[] { 2, 3 }; // page numbers (1‑based)

        // Verify that the input file exists before invoking Delete
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" not found.");
            return;
        }

        try
        {
            // PdfFileEditor does not implement IDisposable, so no using block is needed
            PdfFileEditor editor = new PdfFileEditor();

            // Delete the specified pages and save the result to a new file
            editor.Delete(inputPath, pagesToDelete, outputPath);

            Console.WriteLine($"Pages {string.Join(", ", pagesToDelete)} deleted successfully.");
            Console.WriteLine($"Result saved to \"{outputPath}\".");
        }
        catch (PdfException ex)
        {
            // Handles cases where the file exists but is not a valid PDF or other PDF‑related errors
            Console.Error.WriteLine($"PDF error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // General fallback for any other unexpected errors
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
