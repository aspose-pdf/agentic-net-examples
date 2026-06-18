using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // original PDF
        const string outputPath = "output_deleted.pdf"; // PDF after deletion
        int[] pagesToDelete = new int[] { 2, 3 };       // pages to remove (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // ---------- Delete pages using PdfFileEditor (facade) ----------
        PdfFileEditor editor = new PdfFileEditor();
        bool deleteResult = editor.TryDelete(inputPath, pagesToDelete, outputPath);
        if (!deleteResult)
        {
            Console.Error.WriteLine("Page deletion failed.");
            return;
        }

        // ---------- Get page counts before and after deletion ----------
        int beforeCount;
        int afterCount;

        // PdfViewer provides PageCount property; it implements IDisposable.
        using (PdfViewer viewerBefore = new PdfViewer())
        {
            viewerBefore.BindPdf(inputPath);
            beforeCount = viewerBefore.PageCount;
        }

        using (PdfViewer viewerAfter = new PdfViewer())
        {
            viewerAfter.BindPdf(outputPath);
            afterCount = viewerAfter.PageCount;
        }

        // ---------- Validate that the count decreased ----------
        Console.WriteLine($"Pages before deletion: {beforeCount}");
        Console.WriteLine($"Pages after deletion : {afterCount}");

        if (afterCount < beforeCount)
        {
            Console.WriteLine("Delete operation successful: page count reduced.");
        }
        else
        {
            Console.WriteLine("Delete operation failed: page count not reduced.");
        }
    }
}