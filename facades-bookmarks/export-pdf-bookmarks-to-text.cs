using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfBookmarkEditor, Bookmark, Bookmarks

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "bookmarks.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Initialize the bookmark editor and bind the PDF
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPdfPath);

            // Extract all bookmarks (recursive hierarchy)
            Aspose.Pdf.Facades.Bookmarks rootBookmarks = editor.ExtractBookmarks();

            // Write the outline to a plain‑text file with indentation
            using (StreamWriter writer = new StreamWriter(outputTxtPath))
            {
                WriteBookmarksRecursive(rootBookmarks, 0, writer);
            }

            // No need to call Save on the editor because we only read bookmarks
        }

        Console.WriteLine($"Bookmarks exported to '{outputTxtPath}'.");
    }

    // Recursively writes bookmarks preserving hierarchy indentation
    private static void WriteBookmarksRecursive(Aspose.Pdf.Facades.Bookmarks bookmarks, int level, StreamWriter writer)
    {
        if (bookmarks == null) return;

        foreach (Bookmark bm in bookmarks)
        {
            // Indentation: 4 spaces per level
            string indent = new string(' ', level * 4);
            writer.WriteLine($"{indent}{bm.Title}");

            // Recursively process child bookmarks, if any
            if (bm.ChildItem != null && bm.ChildItem.Count > 0)
            {
                WriteBookmarksRecursive(bm.ChildItem, level + 1, writer);
            }
        }
    }
}