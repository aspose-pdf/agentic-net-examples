using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath   = "source.pdf";
        const string outputPath   = "booklet_custom.pdf";

        // Verify source file exists
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Define custom page ordering for left and right pages in the booklet
        // Example: left side pages and right side pages (1‑based indexing)
        int[] leftPages  = new int[] { 1, 3, 5, 7 };
        int[] rightPages = new int[] { 2, 4, 6, 8 };

        // PdfFileEditor does NOT implement IDisposable, so no using block is needed
        PdfFileEditor editor = new PdfFileEditor();

        // Create booklet with A5 page size using the specified page order arrays
        // The method modifies the source PDF and writes the result to outputPath
        editor.MakeBooklet(sourcePath, outputPath, PageSize.A5, leftPages, rightPages);

        Console.WriteLine($"Booklet created successfully: {outputPath}");
    }
}