using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "range_extracted.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable, so do NOT use a using block
        PdfFileEditor editor = new PdfFileEditor();

        // Define the page range to extract (pages 4 through 9, inclusive)
        int[] pagesToExtract = new int[] { 4, 5, 6, 7, 8, 9 };

        // Extract the specified pages and save to the output file
        // NOTE: The correct overload order is (sourcePath, pageNumbers, outputPath)
        editor.Extract(inputPath, pagesToExtract, outputPath);

        Console.WriteLine($"Pages 4‑9 extracted to '{outputPath}'.");
    }
}
