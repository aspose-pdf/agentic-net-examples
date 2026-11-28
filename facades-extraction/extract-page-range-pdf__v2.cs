using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";
        const string outputPath = "range22_28.pdf";

        // Verify the source PDF exists
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable, so we instantiate it directly
        PdfFileEditor editor = new PdfFileEditor();

        // Extract pages 22 through 28 (inclusive) from the source PDF
        // Correct overload: Extract(string sourceFile, int startPage, int endPage, string outputFile)
        editor.Extract(sourcePath, 22, 28, outputPath);

        Console.WriteLine($"Pages 22‑28 have been extracted to '{outputPath}'.");
    }
}
