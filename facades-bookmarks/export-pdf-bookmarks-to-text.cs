using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // needed for Bookmark and Bookmarks types

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outlineTxt = "bookmarks.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the PDF file to the bookmark editor.
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            bookmarkEditor.BindPdf(inputPdf);

            // Extract the top‑level bookmark collection.
            Bookmarks rootBookmarks = bookmarkEditor.ExtractBookmarks();

            // Write the outline to a plain‑text file, preserving hierarchy via indentation.
            using (StreamWriter writer = new StreamWriter(outlineTxt))
            {
                WriteBookmarks(rootBookmarks, writer, 0);
            }
        }

        Console.WriteLine($"Bookmarks exported to '{outlineTxt}'.");
    }

    // Recursively writes bookmarks with indentation (2 spaces per level).
    static void WriteBookmarks(Bookmarks collection, StreamWriter writer, int level)
    {
        string indent = new string(' ', level * 2);
        foreach (Bookmark bm in collection)
        {
            writer.WriteLine($"{indent}{bm.Title}");
            // In the Facade model child bookmarks are accessed via ChildItems.
            if (bm.ChildItems != null && bm.ChildItems.Count > 0)
            {
                WriteBookmarks(bm.ChildItems, writer, level + 1);
            }
        }
    }
}
