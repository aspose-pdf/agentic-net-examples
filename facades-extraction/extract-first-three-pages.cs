using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "first_three_pages.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable; do NOT use a using block.
            PdfFileEditor editor = new PdfFileEditor();

            // Extract pages 1 through 3 (inclusive) from the source PDF.
            // Correct signature: Extract(string sourcePath, int startPage, int endPage, string outputPath)
            editor.Extract(inputPath, 1, 3, outputPath);

            Console.WriteLine($"Pages 1‑3 extracted to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Extraction failed: {ex.Message}");
        }
    }
}
