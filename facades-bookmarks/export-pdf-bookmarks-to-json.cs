using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Facades; // PdfBookmarkEditor resides here

// Model that will be serialized to JSON
public class BookmarkNode
{
    public string Title { get; set; }
    public int? PageNumber { get; set; }          // null when not set
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

        // Initialize the bookmark editor and bind the PDF
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        try
        {
            editor.BindPdf(inputPdf);

            // Extract all bookmarks (recursive)
            Aspose.Pdf.Facades.Bookmarks rawBookmarks = editor.ExtractBookmarks();

            // Convert to our serializable structure
            List<BookmarkNode> jsonTree = new List<BookmarkNode>();
            foreach (Aspose.Pdf.Facades.Bookmark bm in rawBookmarks)
            {
                jsonTree.Add(ConvertBookmark(bm));
            }

            // Serialize to pretty‑printed JSON
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(jsonTree, jsonOptions);

            // Write JSON to file
            File.WriteAllText(outputJson, json);
            Console.WriteLine($"Bookmarks exported to '{outputJson}'.");
        }
        finally
        {
            // Ensure resources are released
            editor.Close();
        }
    }

    // Recursively transforms Aspose.Pdf.Facades.Bookmark into BookmarkNode
    private static BookmarkNode ConvertBookmark(Aspose.Pdf.Facades.Bookmark bm)
    {
        BookmarkNode node = new BookmarkNode {
            Title = bm.Title,
            PageNumber = bm.PageNumber > 0 ? (int?)bm.PageNumber : null
        };

        // ChildItems may be null; guard against it
        if (bm.ChildItems != null && bm.ChildItems.Count > 0)
        {
            foreach (Aspose.Pdf.Facades.Bookmark child in bm.ChildItems)
            {
                node.Children.Add(ConvertBookmark(child));
            }
        }

        return node;
    }
}