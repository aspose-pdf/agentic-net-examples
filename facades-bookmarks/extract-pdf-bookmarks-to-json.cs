using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;   // PdfBookmarkEditor, Bookmark, Bookmarks

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "bookmarks.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Extract bookmarks using PdfBookmarkEditor (facade API)
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPdfPath);                     // Load the PDF
            Aspose.Pdf.Facades.Bookmarks rootBookmarks = editor.ExtractBookmarks(); // All levels

            // Convert the Aspose bookmark collection into a plain POCO hierarchy
            List<BookmarkNode> hierarchy = new List<BookmarkNode>();
            foreach (Aspose.Pdf.Facades.Bookmark bm in rootBookmarks)
            {
                hierarchy.Add(ConvertBookmark(bm));
            }

            // Serialize the hierarchy to indented JSON
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(hierarchy, jsonOptions);
            File.WriteAllText(outputJsonPath, json);
            Console.WriteLine($"Bookmarks JSON saved to '{outputJsonPath}'.");
        }
    }

    // Recursively maps Aspose.Pdf.Facades.Bookmark to a simple POCO
    static BookmarkNode ConvertBookmark(Aspose.Pdf.Facades.Bookmark bm)
    {
        BookmarkNode node = new BookmarkNode {
            Title = bm.Title,
            PageNumber = bm.PageNumber,
            Children = new List<BookmarkNode>()
        };

        // ChildItems may be null if there are no children
        if (bm.ChildItems != null)
        {
            foreach (Aspose.Pdf.Facades.Bookmark child in bm.ChildItems)
            {
                node.Children.Add(ConvertBookmark(child));
            }
        }

        return node;
    }

    // Plain C# class representing a bookmark node for JSON serialization
    class BookmarkNode
    {
        public string Title { get; set; }
        public int PageNumber { get; set; }
        public List<BookmarkNode> Children { get; set; }
    }
}