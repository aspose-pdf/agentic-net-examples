using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "bookmarks.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Open the PDF and extract bookmarks using PdfBookmarkEditor
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPdfPath);

            // Extract all bookmarks (recursive)
            Bookmarks bookmarks = editor.ExtractBookmarks();

            // Write bookmarks to CSV
            using (StreamWriter writer = new StreamWriter(outputCsvPath, false))
            {
                // CSV header
                writer.WriteLine("Title,Destination,Level");

                // Recursive traversal to capture hierarchy level
                WriteBookmarksRecursive(bookmarks, 0, writer);
            }

            // No need to call Save on the editor because we are only reading.
        }

        Console.WriteLine($"Bookmarks exported to '{outputCsvPath}'.");
    }

    // Recursively writes each bookmark with its hierarchy level
    private static void WriteBookmarksRecursive(Bookmarks bookmarks, int level, StreamWriter writer)
    {
        foreach (Bookmark bm in bookmarks)
        {
            // Resolve destination: prefer explicit Destination, fallback to PageNumber
            string destination = bm.Destination != null ? bm.Destination.ToString() :
                                 bm.PageNumber > 0 ? $"Page {bm.PageNumber}" : string.Empty;

            // Escape commas in title by surrounding with double quotes if needed
            string title = bm.Title?.Contains(",") == true ? $"\"{bm.Title}\"" : bm.Title;

            writer.WriteLine($"{title},{destination},{level}");

            // If the bookmark has child items, recurse with increased level
            if (bm.ChildItem != null && bm.ChildItem.Count > 0)
            {
                WriteBookmarksRecursive(bm.ChildItem, level + 1, writer);
            }
        }
    }
}