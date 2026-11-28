using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath = "document.pdf";
        const string outputPath = "range30_40.pdf";
        const int startPage = 30; // inclusive
        const int endPage   = 40; // inclusive

        // Verify source file exists
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable – do NOT use a using block
            PdfFileEditor editor = new PdfFileEditor();

            // Extract pages 30‑40 (1‑based indexing) to a new PDF
            // Correct overload: Extract(string sourceFile, int startPage, int endPage, string outputFile)
            editor.Extract(sourcePath, startPage, endPage, outputPath);

            Console.WriteLine($"Pages {startPage}-{endPage} extracted to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}
