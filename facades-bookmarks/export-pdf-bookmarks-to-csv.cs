using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfBookmarkEditor, Bookmark, Bookmarks

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputCsv = "bookmarks.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Open the PDF and extract its bookmarks
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPdf);                     // initialize facade with the PDF
            Bookmarks bookmarks = editor.ExtractBookmarks(); // get all bookmarks (recursive)

            // Write bookmarks to CSV
            using (StreamWriter writer = new StreamWriter(outputCsv, false))
            {
                // CSV header
                writer.WriteLine("Title,DestinationPage,Level");

                // Recursive traversal to capture hierarchy level
                WriteBookmarks(bookmarks, 0, writer);
            }
        }

        Console.WriteLine($"Bookmarks exported to '{outputCsv}'.");
    }

    // Recursively writes each bookmark with its hierarchy level
    private static void WriteBookmarks(Bookmarks bookmarks, int level, StreamWriter writer)
    {
        foreach (Bookmark bm in bookmarks)
        {
            // Escape title for CSV (handle commas and quotes)
            string escapedTitle = EscapeForCsv(bm.Title);

            // Destination: use PageNumber if set; otherwise leave empty
            string destination = bm.PageNumber > 0 ? bm.PageNumber.ToString() : string.Empty;

            writer.WriteLine($"{escapedTitle},{destination},{level}");

            // Process child bookmarks, if any
            if (bm.ChildItem != null && bm.ChildItem.Count > 0)
            {
                WriteBookmarks(bm.ChildItem, level + 1, writer);
            }
        }
    }

    // Simple CSV escaping: double quotes are doubled, field is quoted if it contains a comma or quote
    private static string EscapeForCsv(string field)
    {
        if (field == null) return string.Empty;

        bool mustQuote = field.Contains(",") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r");
        string escaped = field.Replace("\"", "\"\"");

        return mustQuote ? $"\"{escaped}\"" : escaped;
    }
}