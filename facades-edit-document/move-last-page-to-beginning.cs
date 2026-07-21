using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string tempPath = "lastPage.pdf";
        const string intermediatePath = "intermediate.pdf";
        const string outputPath = "reordered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Determine total number of pages (1‑based indexing)
        int pageCount;
        using (Document doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count;
        }

        // 1. Extract the last page to a temporary PDF
        PdfFileEditor editor = new PdfFileEditor();
        editor.Extract(inputPath, new int[] { pageCount }, tempPath);

        // 2. Insert the extracted page at the beginning (position 1)
        editor.Insert(inputPath, 1, tempPath, 1, 1, intermediatePath);

        // 3. Delete the original last page (now at position pageCount + 1)
        editor.Delete(intermediatePath, new int[] { pageCount + 1 }, outputPath);

        // Clean up temporary files
        try { File.Delete(tempPath); } catch { }
        try { File.Delete(intermediatePath); } catch { }

        Console.WriteLine($"Pages reordered successfully. Output saved to '{outputPath}'.");
    }
}