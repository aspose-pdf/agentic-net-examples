using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;               // <-- added core namespace for Bookmark classes
using Aspose.Pdf.Facades;

class Program
{
    // Simple DTO for JSON output
    private class BookmarkInfo
    {
        public string Title { get; set; }
        public int Level { get; set; }
        public int PageNumber { get; set; }
    }

    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string jsonPath = "bookmarks.json";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load PDF and work with its bookmarks using PdfBookmarkEditor (facade API)
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(pdfPath);                     // load PDF
            Bookmarks rootBookmarks = editor.ExtractBookmarks(); // get all bookmarks

            var list = new List<BookmarkInfo>();
            TraverseBookmarks(rootBookmarks, 1, list);   // start at level 1

            // Serialize to JSON (indented for readability)
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(list, jsonOptions);
            File.WriteAllText(jsonPath, json);
        }

        Console.WriteLine($"Bookmarks exported to '{jsonPath}'.");
    }

    // Recursive traversal of the bookmark tree
    private static void TraverseBookmarks(Bookmarks bookmarks, int level, List<BookmarkInfo> output)
    {
        foreach (Bookmark bm in bookmarks)
        {
            output.Add(new BookmarkInfo
            {
                Title = bm.Title,
                Level = level,
                PageNumber = bm.PageNumber
            });

            // If the bookmark has children, recurse with increased level
            if (bm.ChildItem != null && bm.ChildItem.Count > 0)
            {
                TraverseBookmarks(bm.ChildItem, level + 1, output);
            }
        }
    }
}
