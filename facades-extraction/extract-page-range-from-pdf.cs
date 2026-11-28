using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "range_output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable – do NOT wrap in using
            PdfFileEditor editor = new PdfFileEditor();

            // Extract pages 1 through 5 (inclusive) from the source PDF
            // Correct overload: Extract(string sourcePdfPath, int startPage, int endPage, string outputPdfPath)
            editor.Extract(inputPath, 1, 5, outputPath);

            Console.WriteLine($"Pages 1‑5 extracted to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}
