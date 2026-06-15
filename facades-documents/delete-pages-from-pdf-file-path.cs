using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does not implement IDisposable; instantiate directly.
        PdfFileEditor editor = new PdfFileEditor();

        // Delete pages 2 through 5 (1‑based indexing).
        int[] pagesToDelete = { 2, 3, 4, 5 };

        bool result = editor.Delete(inputPath, pagesToDelete, outputPath);

        if (result)
            Console.WriteLine($"Pages 2‑5 removed. Output saved to '{outputPath}'.");
        else
            Console.Error.WriteLine("Failed to delete specified pages.");
    }
}