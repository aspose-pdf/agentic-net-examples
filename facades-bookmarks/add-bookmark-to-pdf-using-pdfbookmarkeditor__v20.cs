using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_bookmark.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the bookmark editor (PdfBookmarkEditor does not implement IDisposable)
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

        // Bind the source PDF document
        bookmarkEditor.BindPdf(inputPath);

        // Preserve any existing outline/bookmarks
        bookmarkEditor.CreateBookmarks();

        // Add a new bookmark titled "Final Note" on page 12 (pages are 1‑based)
        // The correct method is CreateBookmarkOfPage, not AddBookmark.
        bookmarkEditor.CreateBookmarkOfPage("Final Note", 12);

        // Save the modified PDF with the new bookmark
        bookmarkEditor.Save(outputPath);

        // Release resources held by the editor
        bookmarkEditor.Close();

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}