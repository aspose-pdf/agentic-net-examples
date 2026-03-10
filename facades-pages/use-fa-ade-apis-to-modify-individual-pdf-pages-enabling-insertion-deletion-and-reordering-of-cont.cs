using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfPageManipulator
{
    static void Main()
    {
        const string sourcePdf = "source.pdf";
        const string insertPdf = "insert.pdf";
        const string tempPdf   = "temp_last_page.pdf";

        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }
        if (!File.Exists(insertPdf))
        {
            Console.Error.WriteLine($"Insert file not found: {insertPdf}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Delete pages 2 and 3 from the source PDF
        // ------------------------------------------------------------
        const string deleteOutput = "source_deleted.pdf";
        var editorDelete = new PdfFileEditor();
        bool deleteSuccess = editorDelete.Delete(sourcePdf, new int[] { 2, 3 }, deleteOutput);
        Console.WriteLine(deleteSuccess ? $"Deleted pages 2‑3: {deleteOutput}" : "Failed to delete pages.");

        // ------------------------------------------------------------
        // 2. Insert pages 1‑2 from insert.pdf after page 1 of source.pdf
        // ------------------------------------------------------------
        const string insertOutput = "source_with_insert.pdf";
        var editorInsert = new PdfFileEditor();
        bool insertSuccess = editorInsert.Insert(sourcePdf, 1, insertPdf, 1, 2, insertOutput);
        Console.WriteLine(insertSuccess ? $"Inserted pages 1‑2 from {insertPdf} after page 1: {insertOutput}" : "Failed to insert pages.");

        // ------------------------------------------------------------
        // 3. Reorder pages: move the last page to the beginning
        // ------------------------------------------------------------
        int lastPageNumber;
        using (var doc = new Document(sourcePdf))
        {
            lastPageNumber = doc.Pages.Count; // 1‑based count
        }

        var extractor = new PdfFileEditor();
        bool extractSuccess = extractor.Extract(sourcePdf, lastPageNumber, lastPageNumber, tempPdf);
        if (!extractSuccess)
        {
            Console.Error.WriteLine("Failed to extract the last page.");
            return;
        }

        const string withoutLastPdf = "source_without_last.pdf";
        var deleter = new PdfFileEditor();
        bool delLastSuccess = deleter.Delete(sourcePdf, new int[] { lastPageNumber }, withoutLastPdf);
        if (!delLastSuccess)
        {
            Console.Error.WriteLine("Failed to delete the last page.");
            return;
        }

        const string reorderedPdf = "source_reordered.pdf";
        var inserter = new PdfFileEditor();
        // Insert location 0 means "before the first page"
        bool reorderSuccess = inserter.Insert(withoutLastPdf, 0, tempPdf, 1, 1, reorderedPdf);
        Console.WriteLine(reorderSuccess ? $"Reordered PDF (last page moved to first): {reorderedPdf}" : "Failed to reorder pages.");

        // Clean up temporary file
        try { File.Delete(tempPdf); } catch { /* ignore */ }
    }
}
