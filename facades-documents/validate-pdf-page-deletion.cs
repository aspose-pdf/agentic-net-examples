using System;
using System.IO;
using Aspose.Pdf.Facades; // Facades API for page editing and deletion

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        // Pages to delete (1‑based indexing)
        int[] pagesToDelete = new int[] { 2, 3 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // ---------- Get page count before deletion ----------
        int beforeCount;
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            pageEditor.BindPdf(inputPath);
            beforeCount = pageEditor.GetPages(); // returns total pages (1‑based)
        }

        // ---------- Perform deletion ----------
        // PdfFileEditor does NOT implement IDisposable, so do NOT wrap it in a using block.
        var fileEditor = new PdfFileEditor();
        bool deleteResult = fileEditor.TryDelete(inputPath, pagesToDelete, outputPath);

        if (!deleteResult)
        {
            Console.Error.WriteLine("Delete operation failed.");
            return;
        }

        // ---------- Get page count after deletion ----------
        int afterCount;
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            pageEditor.BindPdf(outputPath);
            afterCount = pageEditor.GetPages();
        }

        // ---------- Validate ----------
        Console.WriteLine($"Pages before delete: {beforeCount}");
        Console.WriteLine($"Pages after  delete: {afterCount}");

        if (afterCount < beforeCount)
        {
            Console.WriteLine("Delete operation succeeded: page count reduced.");
        }
        else
        {
            Console.WriteLine("Delete operation did not reduce page count.");
        }
    }
}
