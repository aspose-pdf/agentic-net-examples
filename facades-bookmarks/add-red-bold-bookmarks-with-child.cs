using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Determine the number of pages in the source PDF
        int pageCount;
        using (var doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count;
        }

        // Build a list of top‑level bookmarks; keep a reference to the page‑5 bookmark
        var topLevelBookmarks = new List<Bookmark>();
        Bookmark page5Bookmark = null;

        for (int i = 1; i <= pageCount; i++)
        {
            var bm = new Bookmark
            {
                Title = $"Page {i}",
                PageNumber = i,
                BoldFlag = true,
                // Fully qualify System.Drawing.Color to avoid ambiguity with Aspose.Pdf.Color
                TitleColor = System.Drawing.Color.Red
            };

            if (i == 5)
                page5Bookmark = bm;

            topLevelBookmarks.Add(bm);
        }

        // Add a child bookmark under the page‑5 bookmark
        if (page5Bookmark != null)
        {
            var child = new Bookmark
            {
                Title = "Subsection",
                PageNumber = 5,
                BoldFlag = true,
                TitleColor = System.Drawing.Color.Red
            };

            // Ensure the ChildItems collection exists before adding
            if (page5Bookmark.ChildItems == null)
                page5Bookmark.ChildItems = new Bookmarks();

            page5Bookmark.ChildItems.Add(child);
        }

        // Apply the bookmarks using PdfBookmarkEditor
        using (var editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPath);

            // Optional: clear any existing bookmarks
            editor.DeleteBookmarks();

            // Create each top‑level bookmark (children are persisted automatically)
            foreach (var bm in topLevelBookmarks)
            {
                editor.CreateBookmarks(bm);
            }

            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
