using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";
        const string outputPath = "segment_2_8.pdf";

        // Verify the source file exists
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable; do NOT wrap it in a using block
        PdfFileEditor editor = new PdfFileEditor();

        try
        {
            // Extract pages 2 through 8 (inclusive) from the source PDF
            // Correct overload: Extract(string sourceFile, int startPage, int endPage, string outputFile)
            editor.Extract(sourcePath, 2, 8, outputPath);
            Console.WriteLine($"Pages 2‑8 extracted to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}
