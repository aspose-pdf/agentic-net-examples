using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "quarterly_pages.pdf";
        int[] pagesToExtract = new int[] { 1, 11, 21, 31 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does not implement IDisposable; instantiate directly.
        PdfFileEditor editor = new PdfFileEditor();

        try
        {
            // Extract the specified pages and save to a new PDF.
            // NOTE: The correct overload order is (sourcePath, pagesArray, outputPath).
            editor.Extract(inputPath, pagesToExtract, outputPath);
            Console.WriteLine($"Pages extracted successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Extraction failed: {ex.Message}");
        }
    }
}
