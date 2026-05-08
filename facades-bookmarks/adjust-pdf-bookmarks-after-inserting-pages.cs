using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "original.pdf";      // PDF with existing bookmarks
        const string outputPdf  = "updated.pdf";       // Result after insertion and bookmark adjustment
        const int    pagesToAdd = 2;                   // Number of new pages to insert at the beginning

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the original document and insert blank pages at the start
        using (Document doc = new Document(inputPdf))
        {
            // Insert the required number of blank pages at position 1 (1‑based indexing)
            for (int i = 0; i < pagesToAdd; i++)
            {
                // Insert an empty page; the size will be taken from the most frequent page size in the document
                doc.Pages.Insert(1);
            }

            // Adjust bookmarks: shift each bookmark's destination page by the number of inserted pages
            using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor(doc))
            {
                // Extract existing bookmarks as a Bookmarks collection (not an array)
                Bookmarks bookmarks = bookmarkEditor.ExtractBookmarks();

                if (bookmarks != null && bookmarks.Count > 0)
                {
                    // Recursively shift page numbers for every bookmark in the hierarchy
                    foreach (Bookmark topLevel in bookmarks)
                    {
                        ShiftBookmarkPageNumber(topLevel, pagesToAdd);
                    }
                }

                // Remove all old bookmarks
                bookmarkEditor.DeleteBookmarks();

                // Re‑create bookmarks with the updated page numbers.
                // PdfBookmarkEditor.CreateBookmarks expects a single Bookmark, so we add each top‑level bookmark individually.
                if (bookmarks != null && bookmarks.Count > 0)
                {
                    foreach (Bookmark topLevel in bookmarks)
                    {
                        bookmarkEditor.CreateBookmarks(topLevel);
                    }
                }

                // Save the modified document (the editor saves the bound document)
                bookmarkEditor.Save(outputPdf);
            }
        }

        Console.WriteLine($"Document saved with adjusted bookmarks: '{outputPdf}'.");
    }

    /// <summary>
    /// Recursively adds an offset to the PageNumber of a bookmark and all its child items.
    /// </summary>
    private static void ShiftBookmarkPageNumber(Bookmark bm, int offset)
    {
        // PageNumber is 1‑based; only modify if it points to a page (0 means "no destination")
        if (bm.PageNumber > 0)
        {
            bm.PageNumber += offset;
        }

        // Process child bookmarks, if any
        if (bm.ChildItems != null && bm.ChildItems.Count > 0)
        {
            foreach (Bookmark child in bm.ChildItems)
            {
                ShiftBookmarkPageNumber(child, offset);
            }
        }
    }
}
