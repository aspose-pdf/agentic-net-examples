using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfBookmarkEditor, Bookmark, Bookmarks

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the PDF and extract its bookmark hierarchy
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPdf);

            // Extract all bookmarks (recursive)
            Bookmarks bookmarks = editor.ExtractBookmarks();

            Console.WriteLine("Bookmark hierarchy:");
            foreach (Bookmark bm in bookmarks)
            {
                PrintBookmark(bm, 0);
            }

            // No modifications are made, so no need to call Save().
        }
    }

    // Recursively prints a bookmark and its children with indentation
    static void PrintBookmark(Bookmark bm, int level)
    {
        string indent = new string(' ', level * 2);
        Console.WriteLine($"{indent}- Title: {bm.Title}, Page: {bm.PageNumber}");

        // ChildItems may be null if the bookmark has no children
        if (bm.ChildItems != null)
        {
            foreach (Bookmark child in bm.ChildItems)
            {
                PrintBookmark(child, level + 1);
            }
        }
    }
}