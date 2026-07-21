using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "bookmarks.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the PDF and extract bookmarks
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPdf);
            Bookmarks bookmarks = editor.ExtractBookmarks(); // recursive extraction

            // Convert to a serializable hierarchy
            List<BookmarkNode> hierarchy = new List<BookmarkNode>();
            foreach (Bookmark bm in bookmarks)
            {
                hierarchy.Add(ConvertBookmark(bm));
            }

            // Serialize hierarchy to JSON
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(hierarchy, jsonOptions);
            File.WriteAllText(outputJson, json);
        }

        Console.WriteLine($"Bookmarks exported to '{outputJson}'.");
    }

    // Recursively transforms Aspose.Pdf.Facades.Bookmark into a plain DTO
    static BookmarkNode ConvertBookmark(Bookmark bm)
    {
        BookmarkNode node = new BookmarkNode {
            Title = bm.Title,
            PageNumber = bm.PageNumber,
            Children = new List<BookmarkNode>()
        };

        if (bm.ChildItems != null)
        {
            foreach (Bookmark child in bm.ChildItems)
            {
                node.Children.Add(ConvertBookmark(child));
            }
        }

        return node;
    }

    // DTO used for JSON serialization
    class BookmarkNode
    {
        public string Title { get; set; }
        public int PageNumber { get; set; }
        public List<BookmarkNode> Children { get; set; }
    }
}