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
            Aspose.Pdf.Facades.Bookmarks rootBookmarks = editor.ExtractBookmarks();

            using (StreamWriter writer = new StreamWriter(outputTxt))
            {
                foreach (Aspose.Pdf.Facades.Bookmark bm in rootBookmarks)
                {
                    WriteBookmark(bm, writer, 0);
                }
            }
        }

        Console.WriteLine($"Bookmarks exported to '{outputTxt}'.");
    }

    // Recursively writes a bookmark and its children with indentation
    static void WriteBookmark(Aspose.Pdf.Facades.Bookmark bm, StreamWriter writer, int level)
    {
        string indent = new string(' ', level * 2); // 2 spaces per hierarchy level
        writer.WriteLine($"{indent}{bm.Title} (Page {bm.PageNumber})");

        if (bm.ChildItem != null && bm.ChildItem.Count > 0)
        {
            foreach (Aspose.Pdf.Facades.Bookmark child in bm.ChildItem)
            {
                WriteBookmark(child, writer, level + 1);
            }
        }
    }
}