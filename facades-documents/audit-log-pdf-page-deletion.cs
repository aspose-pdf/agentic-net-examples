using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, pages to delete, and output PDF paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        int[] pagesToDelete = new int[] { 2, 3, 5 }; // example page numbers (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Get original page count for audit purposes
        int originalPageCount;
        using (Document srcDoc = new Document(inputPath))
        {
            originalPageCount = srcDoc.Pages.Count;
        }

        // Perform deletion using PdfFileEditor (facade API)
        PdfFileEditor editor = new PdfFileEditor();
        bool deleteSucceeded = editor.TryDelete(inputPath, pagesToDelete, outputPath);

        // Determine how many pages were actually removed
        int pagesRemoved = 0;
        if (deleteSucceeded && File.Exists(outputPath))
        {
            using (Document outDoc = new Document(outputPath))
            {
                int newPageCount = outDoc.Pages.Count;
                pagesRemoved = originalPageCount - newPageCount;
            }
        }
        else
        {
            Console.Error.WriteLine("Page deletion failed.");
        }

        // Log audit information
        Console.WriteLine($"Audit Log - Delete Operation:");
        Console.WriteLine($"  Input file          : {inputPath}");
        Console.WriteLine($"  Requested pages    : {string.Join(", ", pagesToDelete)}");
        Console.WriteLine($"  Original page count: {originalPageCount}");
        Console.WriteLine($"  Output file         : {outputPath}");
        Console.WriteLine($"  New page count      : {originalPageCount - pagesRemoved}");
        Console.WriteLine($"  Pages actually removed: {pagesRemoved}");
    }
}