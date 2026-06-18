using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string originalPdf = "original.pdf";      // PDF to be modified
        const string pagesToInsertPdf = "newPages.pdf"; // PDF containing pages to insert at the beginning
        const string tempPdf = "temp_inserted.pdf";     // intermediate file after insertion
        const string finalPdf = "final.pdf";            // output PDF with adjusted bookmarks

        if (!File.Exists(originalPdf) || !File.Exists(pagesToInsertPdf))
        {
            Console.Error.WriteLine("Input files not found.");
            return;
        }

        // ------------------------------------------------------------
        // Step 1: Extract existing bookmarks (title + destination page)
        // ------------------------------------------------------------
        var originalBookmarks = new List<(string Title, int PageNumber)>();

        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            bookmarkEditor.BindPdf(originalPdf);

            // ExtractBookmarks returns a *Bookmarks* collection, not a single Bookmark.
            Bookmarks topLevelBookmarks = bookmarkEditor.ExtractBookmarks();

            // Helper to traverse bookmarks recursively
            void Traverse(Bookmark bm)
            {
                if (bm == null) return;
                if (!string.IsNullOrEmpty(bm.Title))
                {
                    originalBookmarks.Add((bm.Title, bm.PageNumber));
                }
                // Use the newer ChildItems property (ChildItem is obsolete)
                if (bm.ChildItems != null)
                {
                    foreach (Bookmark child in bm.ChildItems)
                    {
                        Traverse(child);
                    }
                }
            }

            if (topLevelBookmarks != null)
            {
                foreach (Bookmark bm in topLevelBookmarks)
                {
                    Traverse(bm);
                }
            }
        }

        // ------------------------------------------------------------
        // Step 2: Insert new pages at the beginning of the document
        // ------------------------------------------------------------
        int insertedPageCount;
        using (Document newPagesDoc = new Document(pagesToInsertPdf))
        {
            insertedPageCount = newPagesDoc.Pages.Count;
        }

        // Use PdfFileEditor to insert the pages.
        // InsertLocation = 1 (insert before the first page of the original PDF)
        // StartPage = 1, EndPage = insertedPageCount (all pages from the newPages PDF)
        PdfFileEditor fileEditor = new PdfFileEditor();
        bool insertResult = fileEditor.Insert(originalPdf, 1, pagesToInsertPdf, 1, insertedPageCount, tempPdf);
        if (!insertResult)
        {
            Console.Error.WriteLine("Failed to insert pages.");
            return;
        }

        // ------------------------------------------------------------
        // Step 3: Delete existing bookmarks in the new PDF (if any)
        // ------------------------------------------------------------
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            bookmarkEditor.BindPdf(tempPdf);
            bookmarkEditor.DeleteBookmarks(); // remove all old bookmarks
            bookmarkEditor.Save(tempPdf);     // save changes
        }

        // ------------------------------------------------------------
        // Step 4: Re‑create bookmarks with shifted page numbers
        // ------------------------------------------------------------
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            bookmarkEditor.BindPdf(tempPdf);

            foreach (var (title, page) in originalBookmarks)
            {
                int newPage = page + insertedPageCount; // shift destination
                bookmarkEditor.CreateBookmarkOfPage(title, newPage);
            }

            bookmarkEditor.Save(finalPdf);
        }

        // Clean up intermediate file
        try { File.Delete(tempPdf); } catch { }

        Console.WriteLine($"Bookmarks adjusted and PDF saved to '{finalPdf}'.");
    }
}
