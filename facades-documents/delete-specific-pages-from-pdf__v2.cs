using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable – instantiate normally
            PdfFileEditor editor = new PdfFileEditor();

            // Delete pages 2 and 4 (page numbers are 1‑based)
            int[] pagesToDelete = new int[] { 2, 4 };
            // Correct overload: Delete(sourcePath, pagesArray, outputPath)
            editor.Delete(inputPath, pagesToDelete, outputPath);

            Console.WriteLine($"Pages 2 and 4 removed. Result saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during page deletion: {ex.Message}");
        }
    }
}
