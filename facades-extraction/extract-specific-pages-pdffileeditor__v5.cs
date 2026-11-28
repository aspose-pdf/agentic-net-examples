using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // source PDF file
        const string outputPath = "stream_quarter.pdf"; // destination file

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Pages to extract (Aspose.Pdf uses 1‑based indexing)
        int[] pagesToExtract = new int[] { 4, 8, 12, 16 };

        // PdfFileEditor does NOT implement IDisposable; instantiate directly
        PdfFileEditor editor = new PdfFileEditor();

        // Correct overload: Extract(sourcePath, pagesArray, destinationPath)
        editor.Extract(inputPath, pagesToExtract, outputPath);

        Console.WriteLine($"Pages 4,8,12,16 extracted to '{outputPath}'.");
    }
}
