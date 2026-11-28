using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";
        const string outputPath = "selected_range.pdf";
        const int startPage = 10;
        const int endPage = 20;

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // PdfFileEditor does not implement IDisposable; instantiate directly.
        PdfFileEditor editor = new PdfFileEditor();

        // Extract pages 10 through 20 (inclusive) from the source PDF.
        // Correct overload: Extract(string sourceFile, int startPage, int endPage, string outputFile)
        editor.Extract(sourcePath, startPage, endPage, outputPath);

        Console.WriteLine($"Pages {startPage}-{endPage} extracted to '{outputPath}'.");
    }
}
