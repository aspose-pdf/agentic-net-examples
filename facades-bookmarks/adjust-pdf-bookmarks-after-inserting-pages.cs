using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Helper method to collect all bookmarks (flat list) from a Bookmark hierarchy
    static void CollectBookmarks(Bookmark bm, List<(string Title, int PageNumber)> list)
    {
        if (bm == null) return;

        // Add current bookmark if it has a title and a page number
        if (!string.IsNullOrEmpty(bm.Title) && bm.PageNumber > 0)
            list.Add((bm.Title, bm.PageNumber));

        // Recursively process child items (if any) using the new ChildItems property
        if (bm.ChildItems != null)
        {
            foreach (Bookmark child in bm.ChildItems)
                CollectBookmarks(child, list);
        }
    }

    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputPdf  = "output.pdf";         // result PDF
        const int    pagesToAdd = 2;                    // number of pages to insert at the beginning

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // -------------------------------------------------
            // Load the original document and extract existing bookmarks
            // -------------------------------------------------
            using (Document doc = new Document(inputPdf))
            {
                // Extract bookmarks using PdfBookmarkEditor
                PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();
                bookmarkEditor.BindPdf(doc);

                // Extract the root bookmark collection
                Bookmarks rootBookmarks = bookmarkEditor.ExtractBookmarks();

                // Flatten the hierarchy into a list of (title, page) pairs
                List<(string Title, int PageNumber)> originalBookmarks = new List<(string, int)>();
                foreach (Bookmark bm in rootBookmarks)
                {
                    CollectBookmarks(bm, originalBookmarks);
                }

                // -------------------------------------------------
                // Insert blank pages at the beginning of the document
                // -------------------------------------------------
                for (int i = 0; i < pagesToAdd; i++)
                {
                    // Insert creates a new empty page automatically (1‑based index)
                    doc.Pages.Insert(1);
                }

                // -------------------------------------------------
                // Re‑create bookmarks with adjusted page numbers
                // -------------------------------------------------
                // Remove all existing bookmarks
                bookmarkEditor.DeleteBookmarks();

                // Add each bookmark back, shifting its page number by the number of inserted pages
                foreach (var (Title, PageNumber) in originalBookmarks)
                {
                    int newPageNumber = PageNumber + pagesToAdd;
                    bookmarkEditor.CreateBookmarkOfPage(Title, newPageNumber);
                }

                // Save the modified document (bookmarks are saved via the editor)
                bookmarkEditor.Save(outputPdf);
                bookmarkEditor.Close(); // release resources held by the facade
            }

            Console.WriteLine($"Bookmarks adjusted and document saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
