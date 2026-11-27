using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "trimmed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable – instantiate normally.
            PdfFileEditor editor = new PdfFileEditor();

            // Build an array of pages to delete (15 through 22 inclusive).
            int startPage = 15;
            int endPage   = 22;
            int[] pagesToDelete = new int[endPage - startPage + 1];
            for (int i = 0; i < pagesToDelete.Length; i++)
                pagesToDelete[i] = startPage + i;

            // Delete the specified pages and save the result.
            // Correct overload: Delete(string sourceFile, int[] pages, string outputFile)
            editor.Delete(inputPath, pagesToDelete, outputPath);

            Console.WriteLine($"Pages {startPage}‑{endPage} removed. Trimmed file saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during page deletion: {ex.Message}");
        }
    }
}
