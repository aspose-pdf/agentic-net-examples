using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "stream_range_extracted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor provides an overload that works with file‑path strings.
        // Use the (string inputFile, int startPage, int endPage, string outputFile) overload
        // to extract pages 6‑12 (inclusive) from the source PDF.
        PdfFileEditor editor = new PdfFileEditor();
        editor.Extract(inputPath, 6, 12, outputPath);

        Console.WriteLine($"Extracted pages saved to '{outputPath}'.");
    }
}
