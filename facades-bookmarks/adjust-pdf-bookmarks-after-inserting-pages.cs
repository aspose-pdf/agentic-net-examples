using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "original.pdf";      // source PDF with existing bookmarks
        const string outputPdf = "updated.pdf";       // result PDF after insertion
        const int pagesToInsert = 2;                  // number of new pages to add at the beginning

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Insert blank pages at the very start (position 1 is the first page)
            for (int i = 0; i < pagesToInsert; i++)
            {
                doc.Pages.Insert(1); // inserts an empty page
            }

            // -----------------------------------------------------------------
            // Adjust existing bookmarks so they point to the correct pages
            // -----------------------------------------------------------------
            // Bind the document to a PdfBookmarkEditor facade
            PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();
            bookmarkEditor.BindPdf(doc);

            // Extract current bookmarks (returns a collection of Bookmark objects)
            // Store title and original page number for later recreation
            List<(string Title, int PageNumber)> originalBookmarks = new List<(string, int)>();
            foreach (Bookmark bm in bookmarkEditor.ExtractBookmarks())
            {
                // Guard against null titles
                string title = bm.Title ?? $"Bookmark_{bm.PageNumber}";
                originalBookmarks.Add((title, bm.PageNumber));
            }

            // Remove all existing bookmarks
            bookmarkEditor.DeleteBookmarks();

            // Re‑create each bookmark with its page number shifted by the inserted pages
            foreach (var (title, page) in originalBookmarks)
            {
                int newPageNumber = page + pagesToInsert;
                bookmarkEditor.CreateBookmarkOfPage(title, newPageNumber);
            }

            // Save the modified document (PDF format, no SaveOptions needed)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Document saved with adjusted bookmarks to '{outputPdf}'.");
    }
}