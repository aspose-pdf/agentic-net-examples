using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Facades; // Bookmark, Bookmarks are in this namespace

class BookmarkEntry
{
    // Title may be missing in the JSON, so make it nullable to silence CS8618
    public string? Title { get; set; }
    public int PageNumber { get; set; }
    public List<BookmarkEntry>? Children { get; set; }
}

class Program
{
    static void Main()
    {
        const string jsonPath = "bookmarks.json";
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_bookmarked.pdf";

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"PDF file not found: {inputPdf}");
            return;
        }

        // Deserialize JSON into a list of bookmark entries
        List<BookmarkEntry>? entries = JsonSerializer.Deserialize<List<BookmarkEntry>>(File.ReadAllText(jsonPath));

        if (entries == null || entries.Count == 0)
        {
            Console.Error.WriteLine("No bookmarks found in JSON.");
            return;
        }

        // Build a single root Bookmark that contains the whole hierarchy.
        Bookmark rootBookmark = new Bookmark
        {
            Title = "Root",
            Action = "GoTo", // required for facade bookmarks
            // The root itself does not need a page number; children will have theirs.
        };
        rootBookmark.ChildItems.AddRange(BuildBookmarksCollection(entries));

        // Use PdfBookmarkEditor (Facades API) to add the bookmarks to the PDF
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPdf);
            // The facade API creates bookmarks from a SINGLE Bookmark instance.
            editor.CreateBookmarks(rootBookmark);
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Bookmarks imported and saved to '{outputPdf}'.");
    }

    // Recursively creates a Bookmarks collection from the custom BookmarkEntry hierarchy
    static Bookmarks BuildBookmarksCollection(List<BookmarkEntry> entries)
    {
        var result = new Bookmarks();
        if (entries == null) return result;

        foreach (var entry in entries)
        {
            // Guard against null titles – the facade allows null titles.
            var bookmark = new Bookmark
            {
                Title = entry.Title,
                PageNumber = entry.PageNumber,
                Action = "GoTo"
            };

            // Recursively add child bookmarks, if any
            if (entry.Children != null && entry.Children.Count > 0)
            {
                bookmark.ChildItems.AddRange(BuildBookmarksCollection(entry.Children));
            }

            result.Add(bookmark);
        }
        return result;
    }
}
