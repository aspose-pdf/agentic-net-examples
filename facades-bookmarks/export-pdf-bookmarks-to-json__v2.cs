using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf.Facades;          // PdfBookmarkEditor
using Aspose.Pdf;                 // Bookmark, Bookmarks

class Program
{
    // Simple DTO for JSON serialization
    private class BookmarkInfo
    {
        public string Title { get; set; }
        public int Level { get; set; }
        public int PageNumber { get; set; }
    }

    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "bookmarks.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load PDF and bind it to the bookmark editor
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPdfPath);

        // Extract all bookmarks (recursive)
        Bookmarks rootBookmarks = editor.ExtractBookmarks();

        // Flatten hierarchy while preserving level information
        List<BookmarkInfo> flatList = new List<BookmarkInfo>();
        TraverseBookmarks(rootBookmarks, 1, flatList);

        // Serialize to pretty‑printed JSON
        JsonSerializerOptions jsonOpts = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(flatList, jsonOpts);
        File.WriteAllText(outputJsonPath, json);

        // Clean up
        editor.Close();

        Console.WriteLine($"Bookmarks exported to '{outputJsonPath}'.");
    }

    // Recursive traversal of Aspose.Pdf.Bookmarks collection
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