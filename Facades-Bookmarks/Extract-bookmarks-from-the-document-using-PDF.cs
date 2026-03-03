using System;
using System.IO;
using Aspose.Pdf;                 // Bookmark, Bookmarks
using Aspose.Pdf.Facades;        // PdfBookmarkEditor

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

        // Create the bookmark editor facade and bind it to the PDF file
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPdf);

        // Extract all bookmarks (recursive)
        Bookmarks bookmarks = editor.ExtractBookmarks();

        // Output bookmark titles (including hierarchy indentation)
        PrintBookmarks(bookmarks, 0);
    }

    static void PrintBookmarks(Bookmarks bookmarks, int level)
    {
        string indent = new string(' ', level * 2);
        foreach (Bookmark bm in bookmarks)
        {
            Console.WriteLine($"{indent}{bm.Title}");
            // If the bookmark has child items, recurse
            if (bm.ChildItem != null && bm.ChildItem.Count > 0)
                PrintBookmarks(bm.ChildItem, level + 1);
        }
    }
}