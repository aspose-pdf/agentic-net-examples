using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";
        const string outputPath = "multi_array.pdf";

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Pages to extract (1‑based indexing)
        int[] pages = new int[] { 9, 18, 27, 36 };

        // PdfFileEditor does NOT implement IDisposable; do NOT wrap in using
        PdfFileEditor editor = new PdfFileEditor();

        try
        {
            // Extract the specified pages into a new PDF file
            editor.Extract(sourcePath, pages, outputPath);
            Console.WriteLine($"Extracted pages saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}