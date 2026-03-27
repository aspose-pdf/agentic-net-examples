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

        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPdf);
            Bookmarks bookmarks = editor.ExtractBookmarks();

            using (StreamWriter writer = new StreamWriter(outputTxt))
            {
                WriteBookmarks(bookmarks, writer, 0);
            }

            editor.Close();
        }

        Console.WriteLine($"Bookmarks exported to '{outputTxt}'.");
    }

    private static void WriteBookmarks(Bookmarks bookmarks, StreamWriter writer, int level)
    {
        foreach (Bookmark bm in bookmarks)
        {
            string indent = new string(' ', level * 4);
            writer.WriteLine($"{indent}{bm.Title}");
            if (bm.ChildItem != null && bm.ChildItem.Count > 0)
            {
                WriteBookmarks(bm.ChildItem, writer, level + 1);
            }
        }
    }
}