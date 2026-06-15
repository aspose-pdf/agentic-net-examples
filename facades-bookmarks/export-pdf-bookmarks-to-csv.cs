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
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the bookmark editor and bind the PDF
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPdf);

            // Extract all bookmarks (recursive hierarchy)
            Bookmarks bookmarks = editor.ExtractBookmarks();

            // Write bookmarks to CSV
            using (StreamWriter writer = new StreamWriter(outputCsv))
            {
                // CSV header
                writer.WriteLine("Title,Destination,Level");
                WriteBookmarks(bookmarks, writer, 1);
            }

            // Close the editor (optional, using will dispose)
            editor.Close();
        }

        Console.WriteLine($"Bookmarks exported to '{outputCsv}'.");
    }

    // Recursively write bookmarks with their hierarchy level
    private static void WriteBookmarks(Bookmarks bookmarks, StreamWriter writer, int level)
    {
        foreach (Bookmark bm in bookmarks)
        {
            string title = EscapeCsv(bm.Title);
            string destination = GetDestination(bm);
            writer.WriteLine($"{title},{destination},{level}");

            // Process child bookmarks if any
            if (bm.ChildItem != null && bm.ChildItem.Count > 0)
            {
                WriteBookmarks(bm.ChildItem, writer, level + 1);
            }
        }
    }

    // Determine a readable destination (page number or explicit destination)
    private static string GetDestination(Bookmark bm)
    {
        // Prefer page number when available
        if (bm.PageNumber > 0)
            return bm.PageNumber.ToString();

        // Use the Destination object's string representation if present
        if (bm.Destination != null)
            return EscapeCsv(bm.Destination.ToString());

        // No destination information
        return "";
    }

    // Escape CSV fields according to RFC 4180
    private static string EscapeCsv(string field)
    {
        if (field == null)
            return "";

        bool mustQuote = field.Contains(",") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r");
        if (mustQuote)
        {
            string escaped = field.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }
        return field;
    }
}