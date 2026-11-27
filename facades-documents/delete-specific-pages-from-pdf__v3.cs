using System;
using System.IO;
using System.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_deleted_pages.pdf";

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable – do NOT wrap in using
        PdfFileEditor editor = new PdfFileEditor();

        // Delete pages 10 through 15 (inclusive). The Delete overload expects the page numbers
        // array as the second argument and the output file path as the third argument.
        int[] pagesToDelete = Enumerable.Range(10, 6).ToArray(); // 10,11,12,13,14,15
        editor.Delete(inputPath, pagesToDelete, outputPath);

        Console.WriteLine($"Pages 10‑15 removed. Result saved to '{outputPath}'.");
    }
}
