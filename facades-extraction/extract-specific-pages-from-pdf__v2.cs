using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";
        const string outputPath = "custom_pages.pdf";

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // PdfFileEditor does not implement IDisposable; instantiate directly.
        PdfFileEditor editor = new PdfFileEditor();

        // Pages to extract (1‑based indexing)
        int[] pagesToExtract = new int[] { 1, 5, 9, 13 };

        try
        {
            // Correct parameter order: source file, page numbers array, output file
            editor.Extract(sourcePath, pagesToExtract, outputPath);
            Console.WriteLine($"Pages extracted successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Extraction failed: {ex.Message}");
        }
    }
}
