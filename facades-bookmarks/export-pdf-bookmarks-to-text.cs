using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "bookmarks.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the PDF and extract its bookmarks
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPdf);
            var bookmarks = editor.ExtractBookmarks(); // all levels

            // Write the outline to a plain‑text file with indentation
            using (StreamWriter writer = new StreamWriter(outputTxt))
            {
                WriteBookmarks(bookmarks, writer, 0);
            }
        }

        Console.WriteLine($"Bookmarks exported to '{outputTxt}'.");
    }

    // Recursive helper to write bookmarks preserving hierarchy
    static void WriteBookmarks(Aspose.Pdf.Facades.Bookmarks bookmarks, StreamWriter writer, int level)
    {
        foreach (Aspose.Pdf.Facades.Bookmark bm in bookmarks)
        {
            string indent = new string(' ', level * 4); // 4 spaces per level
            writer.WriteLine($"{indent}{bm.Title}");

            // If the bookmark has children, recurse with increased level
            if (bm.ChildItem != null && bm.ChildItem.Count > 0)
            {
                WriteBookmarks(bm.ChildItem, writer, level + 1);
            }
        }
    }
}