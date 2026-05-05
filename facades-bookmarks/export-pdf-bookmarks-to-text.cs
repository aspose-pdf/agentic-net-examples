using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputTxt = "bookmarks.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the PDF file to the bookmark editor
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPdf);

            // Extract all bookmarks (including nested ones)
            Aspose.Pdf.Facades.Bookmarks rootBookmarks = editor.ExtractBookmarks();

            // Write the outline to a plain‑text file with indentation
            using (StreamWriter writer = new StreamWriter(outputTxt))
            {
                WriteBookmarksRecursive(rootBookmarks, writer, 0);
            }

            // Close the editor (optional, Dispose will be called by using)
            editor.Close();
        }

        Console.WriteLine($"Bookmarks exported to '{outputTxt}'.");
    }

    // Recursively writes bookmarks, adding two spaces per hierarchy level
    static void WriteBookmarksRecursive(Aspose.Pdf.Facades.Bookmarks bookmarks,
                                        StreamWriter writer,
                                        int level)
    {
        foreach (Aspose.Pdf.Facades.Bookmark bm in bookmarks)
        {
            string indent = new string(' ', level * 2);
            writer.WriteLine($"{indent}{bm.Title}");

            // If the bookmark has child items, process them recursively
            if (bm.ChildItem != null && bm.ChildItem.Count > 0)
            {
                WriteBookmarksRecursive(bm.ChildItem, writer, level + 1);
            }
        }
    }
}