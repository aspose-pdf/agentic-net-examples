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

        // Load PDF and extract bookmarks using the Facades API
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            bookmarkEditor.BindPdf(inputPdf);

            // Extract the full bookmark hierarchy
            Bookmarks topBookmarks = bookmarkEditor.ExtractBookmarks();

            // Convert the Aspose bookmark hierarchy to a POCO tree
            List<BookmarkNode> jsonTree = ConvertBookmarks(topBookmarks);

            // Serialize the tree to indented JSON
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(jsonTree, jsonOptions);

            // Write JSON to file
            File.WriteAllText(outputJson, json);
            Console.WriteLine($"Bookmarks exported to '{outputJson}'.");
        }
    }

    // Recursively transforms a collection of Bookmark into a list of POCO nodes
    static List<BookmarkNode> ConvertBookmarks(IList<Bookmark> collection)
    {
        var list = new List<BookmarkNode>();
        if (collection == null) return list;

        foreach (Bookmark bm in collection)
        {
            var node = new BookmarkNode
            {
                Title = bm.Title,
                PageNumber = bm.PageNumber,
                // In the Facade model child bookmarks are accessed via ChildItems
                Children = ConvertBookmarks(bm.ChildItems)
            };
            list.Add(node);
        }

        return list;
    }

    // POCO representing a bookmark for JSON serialization
    public class BookmarkNode
    {
        public string Title { get; set; }
        public int PageNumber { get; set; }
        public List<BookmarkNode> Children { get; set; }
    }
}
