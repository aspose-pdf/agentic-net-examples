using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "bookmarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfBookmarkEditor does NOT implement IDisposable, so we use a plain instance.
        var bookmarkEditor = new PdfBookmarkEditor();
        bookmarkEditor.BindPdf(inputPath);

        // Build the bookmark hierarchy using the facade Bookmark class.
        // Parent bookmark "Part A" on page 2.
        var partABookmark = new Bookmark
        {
            Title = "Part A",
            PageNumber = 2,
            Action = "GoTo",   // default action for a page link
            Open = true
        };

        // Child bookmark "Chapter A1" on page 3.
        var chapterA1Bookmark = new Bookmark
        {
            Title = "Chapter A1",
            PageNumber = 3,
            Action = "GoTo"
        };

        // Sub‑child bookmark "Section A1.1" on page 4.
        var sectionA11Bookmark = new Bookmark
        {
            Title = "Section A1.1",
            PageNumber = 4,
            Action = "GoTo"
        };

        // Nest the bookmarks.
        chapterA1Bookmark.ChildItems.Add(sectionA11Bookmark);
        partABookmark.ChildItems.Add(chapterA1Bookmark);

        // Add the whole hierarchy to the PDF.
        bookmarkEditor.CreateBookmarks(partABookmark);

        // Save the modified PDF.
        bookmarkEditor.Save(outputPath);
        bookmarkEditor.Close();

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
