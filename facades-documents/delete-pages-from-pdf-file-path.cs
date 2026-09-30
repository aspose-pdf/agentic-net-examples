using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable, so we do NOT wrap it in a using block
            PdfFileEditor editor = new PdfFileEditor();

            // Delete pages 2 through 5 (inclusive). Use the overload that accepts an array of page numbers.
            // NOTE: In the version of Aspose.Pdf used, the overload signature is Delete(string inputPath, int[] pagesToDelete, string outputPath).
            int[] pagesToDelete = { 2, 3, 4, 5 };
            editor.Delete(inputPath, pagesToDelete, outputPath);

            Console.WriteLine($"Pages 2‑5 removed. Result saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
