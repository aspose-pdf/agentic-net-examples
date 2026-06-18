using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string tempPath = "temp_reordered.pdf";
        const string outputPath = "reordered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Get total number of pages (1‑based indexing)
        int pageCount;
        using (Document doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count;
        }

        // Insert the last page at the beginning of the document
        PdfFileEditor editor = new PdfFileEditor();
        bool insertOk = editor.Insert(inputPath, 0, inputPath, pageCount, pageCount, tempPath);
        if (!insertOk)
        {
            Console.Error.WriteLine("Failed to insert the last page at the beginning.");
            return;
        }

        // After insertion the original last page is now at position pageCount + 1
        bool deleteOk = editor.Delete(tempPath, new int[] { pageCount + 1 }, outputPath);
        if (!deleteOk)
        {
            Console.Error.WriteLine("Failed to delete the original last page.");
            return;
        }

        // Remove the temporary file
        try { File.Delete(tempPath); } catch { }

        Console.WriteLine($"Reordered PDF saved to '{outputPath}'.");
    }
}