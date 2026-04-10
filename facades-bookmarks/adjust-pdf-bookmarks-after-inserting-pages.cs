using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "original.pdf";      // source PDF with existing bookmarks
        const string outputPdf = "updated.pdf";       // result PDF after inserting pages
        const int    pagesToInsert = 2;               // number of blank pages to add at the beginning

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // ------------------------------------------------------------
            // 1. Insert blank pages at the very beginning (position = 1)
            // ------------------------------------------------------------
            for (int i = 0; i < pagesToInsert; i++)
            {
                // Insert() adds an empty page; 1‑based indexing is required
                doc.Pages.Insert(1);
            }

            // ------------------------------------------------------------
            // 2. Extract existing bookmarks, adjust their page numbers,
            //    delete old bookmarks and recreate them with the new offsets.
            // ------------------------------------------------------------
            using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor(doc))
            {
                // Extract the whole bookmark collection (top‑level nodes)
                Bookmarks topBookmarks = bookmarkEditor.ExtractBookmarks();

                // Flatten the tree into a list while preserving hierarchy order
                List<Bookmark> flatList = new List<Bookmark>();
                void Collect(Bookmark bm)
                {
                    if (bm == null) return;
                    flatList.Add(bm);
                    if (bm.ChildItems != null)
                    {
                        foreach (Bookmark child in bm.ChildItems)
                        {
                            Collect(child);
                        }
                    }
                }

                if (topBookmarks != null)
                {
                    foreach (Bookmark bm in topBookmarks)
                    {
                        Collect(bm);
                    }
                }

                // Remove all existing bookmarks
                bookmarkEditor.DeleteBookmarks();

                // Re‑create each bookmark with the page number shifted by the inserted pages
                foreach (Bookmark bm in flatList)
                {
                    // Guard against bookmarks without a valid page number
                    if (bm.PageNumber > 0)
                    {
                        int newPage = bm.PageNumber + pagesToInsert;
                        bookmarkEditor.CreateBookmarkOfPage(bm.Title, newPage);
                    }
                }
            }

            // ------------------------------------------------------------
            // 3. Save the modified document
            // ------------------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Document saved with updated bookmarks: {outputPdf}");
    }
}
