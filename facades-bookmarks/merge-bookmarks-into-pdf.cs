using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string targetPdfPath = "target.pdf";   // PDF that will receive the bookmarks
        const string sourcePdfPath = "source.pdf";   // PDF containing the bookmarks to import
        const string outputPdfPath = "merged_bookmarks.pdf";

        // Verify that both input files exist
        if (!File.Exists(targetPdfPath) || !File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the target PDF inside a using block for deterministic disposal
        using (Document targetDoc = new Document(targetPdfPath))
        {
            // Bind the target document to a bookmark editor
            PdfBookmarkEditor targetEditor = new PdfBookmarkEditor();
            targetEditor.BindPdf(targetDoc);

            // Bind the source PDF to another bookmark editor and extract its bookmarks
            PdfBookmarkEditor sourceEditor = new PdfBookmarkEditor();
            sourceEditor.BindPdf(sourcePdfPath);
            Bookmarks sourceBookmarks = sourceEditor.ExtractBookmarks(); // all levels, ordered

            // Append each top‑level bookmark (including its child hierarchy) to the target
            foreach (Bookmark bm in sourceBookmarks)
            {
                // CreateBookmarks adds the entire bookmark tree preserving order
                targetEditor.CreateBookmarks(bm);
            }

            // Save the modified target PDF with the merged bookmarks
            targetEditor.Save(outputPdfPath);
        }

        Console.WriteLine($"Bookmarks merged successfully. Output saved to '{outputPdfPath}'.");
    }
}