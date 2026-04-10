using System;
using System.IO;
using Aspose.Pdf;                 // Core PDF API (Document)
using Aspose.Pdf.Facades;        // Facade API (PdfBookmarkEditor, Bookmark, Bookmarks)

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

        // Load the PDF inside a using block (document-disposal-with-using rule)
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the bookmark editor and bind the loaded document
            PdfBookmarkEditor editor = new PdfBookmarkEditor();
            editor.BindPdf(doc);

            // Extract all bookmarks (recursive hierarchy)
            Bookmarks bookmarks = editor.ExtractBookmarks();

            // Write the outline to a plain‑text file, preserving hierarchy via indentation
            using (StreamWriter writer = new StreamWriter(outputTxt))
            {
                WriteBookmarks(bookmarks, writer, 0);
            }
        }

        Console.WriteLine($"Bookmarks exported to '{outputTxt}'.");
    }

    // Recursively writes bookmarks; each level adds two spaces of indentation
    static void WriteBookmarks(Bookmarks bookmarks, StreamWriter writer, int level)
    {
        if (bookmarks == null) return;

        foreach (Bookmark bm in bookmarks)
        {
            string indent = new string(' ', level * 2);
            writer.WriteLine($"{indent}{bm.Title}");

            // If the bookmark has children, recurse with increased level
            if (bm.ChildItem != null && bm.ChildItem.Count > 0)
            {
                WriteBookmarks(bm.ChildItem, writer, level + 1);
            }
        }
    }
}