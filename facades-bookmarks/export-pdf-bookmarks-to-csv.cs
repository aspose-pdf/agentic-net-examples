using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputCsv = "bookmarks.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use PdfBookmarkEditor to work with bookmarks
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Bind the PDF document
            editor.BindPdf(inputPdf);

            // Extract all bookmarks (recursive)
            Bookmarks bookmarks = editor.ExtractBookmarks();

            // Write CSV file
            using (StreamWriter writer = new StreamWriter(outputCsv))
            {
                // CSV header
                writer.WriteLine("Title,Destination,Level");

                // Recursively write each bookmark
                WriteBookmarks(writer, bookmarks, 0);
            }
        }

        Console.WriteLine($"Bookmarks exported to '{outputCsv}'.");
    }

    // Recursive helper to write bookmarks with their hierarchy level
    static void WriteBookmarks(StreamWriter writer, Bookmarks bookmarks, int level)
    {
        foreach (Bookmark bm in bookmarks)
        {
            // Destination: use page number if available; otherwise empty
            string destination = bm.PageNumber > 0 ? bm.PageNumber.ToString() : string.Empty;

            // Escape commas in title
            string title = bm.Title?.Replace(",", "&#44;") ?? string.Empty;

            writer.WriteLine($"{title},{destination},{level}");

            // If the bookmark has child items, recurse with increased level
            if (bm.ChildItem != null && bm.ChildItem.Count > 0)
            {
                WriteBookmarks(writer, bm.ChildItem, level + 1);
            }
        }
    }
}