using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string tempPath   = "temp_inserted.pdf";
        const string outputPath = "reordered.pdf";

        // Ensure the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Determine the total number of pages in the source PDF
        int pageCount;
        using (Document srcDoc = new Document(inputPath))
        {
            pageCount = srcDoc.Pages.Count; // 1‑based page count
        }

        // Step 1: Insert the last page at the beginning of the document.
        // InsertLocation = 1 (before the first page), startPage = endPage = pageCount (last page).
        PdfFileEditor editor = new PdfFileEditor();
        editor.Insert(inputPath, 1, inputPath, pageCount, pageCount, tempPath);

        // Step 2: Delete the original last page, which is now at position pageCount + 1.
        int[] pagesToDelete = new int[] { pageCount + 1 };
        editor.Delete(tempPath, pagesToDelete, outputPath);

        // Clean up the intermediate file
        try { File.Delete(tempPath); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"Reordered PDF saved to '{outputPath}'.");
    }
}