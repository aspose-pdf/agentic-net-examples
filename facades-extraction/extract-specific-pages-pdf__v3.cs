using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "three_pages.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable; instantiate normally
            PdfFileEditor editor = new PdfFileEditor();

            // Pages to extract (1‑based indexing)
            int[] pagesToExtract = new int[] { 14, 28, 42 };

            // Correct argument order: source file, pages array, destination file
            editor.Extract(inputPath, pagesToExtract, outputPath);

            Console.WriteLine($"Pages 14, 28, 42 extracted to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}
