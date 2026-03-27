using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputCsv = "bookmarks.csv";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPath);
            Bookmarks bookmarks = editor.ExtractBookmarks();

            using (StreamWriter writer = new StreamWriter(outputCsv))
            {
                writer.WriteLine("Title,Destination,Level");
                WriteBookmarks(writer, bookmarks, 1);
            }
        }

        Console.WriteLine("Bookmarks exported to " + outputCsv);
    }

    private static void WriteBookmarks(StreamWriter writer, Bookmarks bookmarks, int level)
    {
        foreach (Bookmark bm in bookmarks)
        {
            string title = bm.Title != null ? bm.Title.Replace("\"", "\"\"") : string.Empty;
            string destination = GetDestination(bm);
            writer.WriteLine($"\"{title}\",\"{destination}\",{level}");

            if (bm.ChildItem != null && bm.ChildItem.Count > 0)
            {
                WriteBookmarks(writer, bm.ChildItem, level + 1);
            }
        }
    }

    private static string GetDestination(Bookmark bm)
    {
        if (!String.IsNullOrEmpty(bm.Destination))
        {
            return bm.Destination;
        }
        if (bm.PageNumber > 0)
        {
            return "Page " + bm.PageNumber;
        }
        return string.Empty;
    }
}