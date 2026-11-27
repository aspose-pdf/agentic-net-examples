using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_bookmarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfBookmarkEditor does not implement IDisposable, so we manage its lifetime manually.
        var editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPath);

        // ----- Build the bookmark hierarchy -----
        // Parent bookmark (page 1)
        var parent = new Bookmark
        {
            Title = "Main Topics",
            PageNumber = 1,
            Action = "GoTo" // required for a page‑jump bookmark
        };

        // Child bookmarks (pages 2‑4)
        var childA = new Bookmark { Title = "Topic A", PageNumber = 2, Action = "GoTo" };
        var childB = new Bookmark { Title = "Topic B", PageNumber = 3, Action = "GoTo" };
        var childC = new Bookmark { Title = "Topic C", PageNumber = 4, Action = "GoTo" };

        // Attach children to the parent via the ChildItems collection.
        parent.ChildItems.Add(childA);
        parent.ChildItems.Add(childB);
        parent.ChildItems.Add(childC);

        // Create the bookmarks in the PDF. The overload that accepts a single Bookmark
        // also handles any nested ChildItems.
        editor.CreateBookmarks(parent);

        // ----- Save the result -----
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
