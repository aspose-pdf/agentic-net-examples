using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;          // PdfBookmarkEditor
using Aspose.Pdf.Facades;          // Bookmark, Bookmarks

class BookmarkNode
{
    public string Title { get; set; }
    public int Level { get; set; }
    public List<BookmarkNode> Children { get; set; } = new List<BookmarkNode>();
}

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

        // Bind the PDF and extract its bookmarks
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPdf);
            // Extract all bookmarks (recursive)
            var bookmarks = editor.ExtractBookmarks();

            // Convert to a hierarchical DTO suitable for JSON serialization
            var rootNodes = new List<BookmarkNode>();
            foreach (var bm in bookmarks)
            {
                rootNodes.Add(ConvertBookmark(bm));
            }

            // Serialize to pretty‑printed JSON
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(rootNodes, jsonOptions);

            // Write JSON to file
            File.WriteAllText(outputJson, json);
            Console.WriteLine($"Bookmarks exported to '{outputJson}'.");
        }
    }

    // Recursively convert Aspose.Pdf.Facades.Bookmark to BookmarkNode
    static BookmarkNode ConvertBookmark(Bookmark bm)
    {
        BookmarkNode node = new BookmarkNode {
            Title = bm.Title,
            Level = bm.Level
        };

        // ChildItems may be null; guard against it
        if (bm.ChildItems != null)
        {
            foreach (var child in bm.ChildItems)
            {
                node.Children.Add(ConvertBookmark(child));
            }
        }

        return node;
    }
}