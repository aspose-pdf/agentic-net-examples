using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputCsv = "bookmarks.csv"; // CSV works with Excel

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Extract bookmarks from the PDF
        var bookmarkEditor = new PdfBookmarkEditor();
        bookmarkEditor.BindPdf(inputPdf);
        Bookmarks bookmarks = bookmarkEditor.ExtractBookmarks();
        bookmarkEditor.Close();

        // Create a CSV file that Excel can open
        using (var writer = new StreamWriter(outputCsv))
        {
            // Write header row
            writer.WriteLine("Title,Level,Destination");

            // Write bookmark data recursively
            int currentRow = 1; // not used for CSV but kept for compatibility
            foreach (Bookmark bm in bookmarks)
            {
                WriteBookmark(bm, 1, writer);
            }
        }

        Console.WriteLine($"Bookmarks exported to {outputCsv}");
    }

    static void WriteBookmark(Bookmark bm, int level, StreamWriter writer)
    {
        // Escape commas in the title by surrounding with double quotes if needed
        string title = bm.Title?.Contains(",") == true ? $"\"{bm.Title}\"" : bm.Title;
        string destination = bm.PageNumber > 0 ? $"Page {bm.PageNumber}" : (string.IsNullOrEmpty(bm.Action) ? "" : bm.Action);
        destination = destination?.Contains(",") == true ? $"\"{destination}\"" : destination;
        writer.WriteLine($"{title},{level},{destination}");

        if (bm.ChildItem != null && bm.ChildItem.Count > 0)
        {
            foreach (Bookmark child in bm.ChildItem)
            {
                WriteBookmark(child, level + 1, writer);
            }
        }
    }
}
