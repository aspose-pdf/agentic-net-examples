using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfBookmarkEditor, Bookmark, Bookmarks

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputCsv = "bookmarks.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Extract bookmarks using PdfBookmarkEditor (facade API)
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPdf);                     // Load PDF into the editor
            Bookmarks bookmarks = editor.ExtractBookmarks(); // Get all bookmarks (recursive)

            using (StreamWriter writer = new StreamWriter(outputCsv))
            {
                // CSV header
                writer.WriteLine("Title,DestinationPage,Level");

                // Write each bookmark with its hierarchy level
                foreach (Bookmark bm in bookmarks)
                {
                    WriteBookmark(bm, writer, 1);
                }
            }
        }

        Console.WriteLine($"Bookmarks exported to '{outputCsv}'.");
    }

    // Recursive helper to write a bookmark and its children
    static void WriteBookmark(Bookmark bm, StreamWriter writer, int level)
    {
        // Destination page is stored in the PageNumber property (0 if not set)
        int destPage = bm.PageNumber;

        // Escape double quotes for CSV compliance
        string title = bm.Title?.Replace("\"", "\"\"") ?? string.Empty;

        // Write CSV line: Title (quoted), DestinationPage, Level
        writer.WriteLine($"\"{title}\",{destPage},{level}");

        // Process child bookmarks, if any
        if (bm.ChildItem != null && bm.ChildItem.Count > 0)
        {
            foreach (Bookmark child in bm.ChildItem)
            {
                WriteBookmark(child, writer, level + 1);
            }
        }
    }
}