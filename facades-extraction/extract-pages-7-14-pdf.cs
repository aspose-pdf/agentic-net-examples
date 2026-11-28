using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "stream_7_14.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does not implement IDisposable, so it is not wrapped in a using block.
        PdfFileEditor editor = new PdfFileEditor();

        // Correct overload: Extract(string sourcePdfPath, int startPage, int endPage, string outputPdfPath)
        // Extract pages 7 through 14 (inclusive) and save to the specified output file.
        editor.Extract(inputPath, 7, 14, outputPath);

        Console.WriteLine($"Pages 7‑14 extracted to '{outputPath}'.");
    }
}