using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string targetPdfPath   = "target.pdf";          // PDF that will receive the bookmarks
        const string sourcePdfPath   = "source.pdf";          // PDF whose bookmarks we want to import
        const string outputPdfPath   = "merged_bookmarks.pdf";

        // Ensure source files exist
        if (!System.IO.File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target PDF not found: {targetPdfPath}");
            return;
        }
        if (!System.IO.File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        // ---------- Bind the target PDF ----------
        PdfBookmarkEditor targetEditor = new PdfBookmarkEditor();
        targetEditor.BindPdf(targetPdfPath);

        // ---------- Extract bookmarks from the external PDF ----------
        PdfBookmarkEditor sourceEditor = new PdfBookmarkEditor();
        sourceEditor.BindPdf(sourcePdfPath);
        Bookmarks sourceBookmarks = sourceEditor.ExtractBookmarks(); // all levels, ordered

        // ---------- Merge bookmarks preserving order ----------
        // Each top‑level bookmark (with its child hierarchy) is added to the target.
        foreach (Bookmark bm in sourceBookmarks)
        {
            // CreateBookmarks adds the supplied bookmark (and its children) to the document.
            targetEditor.CreateBookmarks(bm);
        }

        // ---------- Save the result ----------
        targetEditor.Save(outputPdfPath);

        // Clean up facades
        targetEditor.Close();
        sourceEditor.Close();

        Console.WriteLine($"Bookmarks merged successfully. Output saved to '{outputPdfPath}'.");
    }
}