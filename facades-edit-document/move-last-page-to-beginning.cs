using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "reordered.pdf";
        const string tempExtract = "lastpage_temp.pdf";
        const string tempInserted = "inserted_temp.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine total number of pages in the source PDF
        int pageCount;
        using (Document srcDoc = new Document(inputPath))
        {
            pageCount = srcDoc.Pages.Count; // 1‑based indexing
        }

        // Step 1: Extract the last page to a temporary PDF
        PdfFileEditor editor = new PdfFileEditor();
        editor.Extract(inputPath, new int[] { pageCount }, tempExtract);

        // Step 2: Insert the extracted page at the beginning of the original PDF
        // InsertLocation = 0 inserts before the first page
        editor.Insert(inputPath, 0, tempExtract, new int[] { 1 }, tempInserted);

        // Step 3: Delete the original last page (now at position pageCount + 1)
        editor.Delete(tempInserted, new int[] { pageCount + 1 }, outputPath);

        // Clean up temporary files
        try { File.Delete(tempExtract); } catch { }
        try { File.Delete(tempInserted); } catch { }

        Console.WriteLine($"Reordered PDF saved to '{outputPath}'.");
    }
}