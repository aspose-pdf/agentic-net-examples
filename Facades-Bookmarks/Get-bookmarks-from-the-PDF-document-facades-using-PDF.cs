using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade classes for PDF operations
using Aspose.Pdf;           // Core PDF types (Bookmark, Bookmarks)

class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the bookmark editor facade and bind the PDF file
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();
        bookmarkEditor.BindPdf(inputPdf);

        // Extract all bookmarks from the document
        Bookmarks bookmarks = bookmarkEditor.ExtractBookmarks();

        // Output each bookmark title (including nested bookmarks)
        foreach (Bookmark bm in bookmarks)
        {
            PrintBookmark(bm, 0);
        }

        // Release resources held by the facade
        bookmarkEditor.Close();
    }

    // Recursive helper to display bookmark hierarchy with indentation
    static void PrintBookmark(Bookmark bm, int level)
    {
        string indent = new string(' ', level * 2);
        Console.WriteLine($"{indent}{bm.Title}");

        // If the bookmark has child items, iterate them recursively
        if (bm.ChildItem != null && bm.ChildItem.Count > 0)
        {
            foreach (Bookmark child in bm.ChildItem)
            {
                PrintBookmark(child, level + 1);
            }
        }
    }
}