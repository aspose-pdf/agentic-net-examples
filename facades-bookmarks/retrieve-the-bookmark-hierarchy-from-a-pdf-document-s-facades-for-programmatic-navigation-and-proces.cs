using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Initialize the bookmark editor facade and bind the PDF file
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPdf);

            // Extract all bookmarks (full hierarchy)
            Bookmarks bookmarks = editor.ExtractBookmarks();

            // Print the hierarchy to the console
            PrintBookmarks(bookmarks, 0);
        }
    }

    // Recursive helper to display bookmark titles with indentation
    static void PrintBookmarks(Bookmarks collection, int depth)
    {
        string indent = new string(' ', depth * 2);
        foreach (Bookmark bm in collection)
        {
            Console.WriteLine($"{indent}{bm.Title}");
            // ChildItem holds nested bookmarks; recurse if present
            if (bm.ChildItem != null && bm.ChildItem.Count > 0)
            {
                PrintBookmarks(bm.ChildItem, depth + 1);
            }
        }
    }
}