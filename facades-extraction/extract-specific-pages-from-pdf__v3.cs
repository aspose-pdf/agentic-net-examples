using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";
        const string outputPath = "pages_set.pdf";

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // PdfFileEditor does not implement IDisposable; instantiate directly.
        PdfFileEditor editor = new PdfFileEditor();

        // Pages to extract (1‑based indexing as required by Aspose.Pdf).
        int[] pagesToExtract = new int[] { 11, 22, 33 };

        try
        {
            // Correct overload: Extract(sourcePath, pagesArray, outputPath)
            editor.Extract(sourcePath, pagesToExtract, outputPath);
            Console.WriteLine($"Pages extracted successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}
