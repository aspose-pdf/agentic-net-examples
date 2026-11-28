using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath = "input.pdf";
        const string outputPath = "mid_section.pdf";
        const int startPage = 20;
        const int endPage   = 30;

        // Verify source file exists
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable; instantiate directly
        PdfFileEditor editor = new PdfFileEditor();

        try
        {
            // Extract pages startPage through endPage (inclusive) to a new PDF
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
