using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfBookmarkEditor, Bookmark, Bookmarks
using Aspose.Pdf;          // Bookmark (shared namespace)

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

        // Bind the PDF to the bookmark editor and extract the bookmark hierarchy.
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPdf);
            Bookmarks rootBookmarks = editor.ExtractBookmarks();

            // Write CSV header and each bookmark (including nested ones) with its level.
            using (StreamWriter writer = new StreamWriter(outputCsv, false, System.Text.Encoding.UTF8))
            {
                writer.WriteLine("Title,Destination,Level");
                foreach (Bookmark bm in rootBookmarks)
                {
                    WriteBookmark(writer, bm, 1);
                }
            }
        }

        Console.WriteLine($"Bookmarks exported to '{outputCsv}'.");
    }

    // Recursively writes a bookmark and its children to the CSV.
    static void WriteBookmark(StreamWriter writer, Bookmark bm, int level)
    {
        // Destination: use the page number if it is set (>0); otherwise leave empty.
        string destination = bm.PageNumber > 0 ? bm.PageNumber.ToString() : string.Empty;

        // Escape commas and quotes in the title for CSV compliance.
        string title = bm.Title ?? string.Empty;
        title = title.Replace("\"", "\"\"");
        if (title.Contains(","))
        {
            title = $"\"{title}\"";
        }

        writer.WriteLine($"{title},{destination},{level}");

        // Process child bookmarks, if any, increasing the level.
        if (bm.ChildItem != null && bm.ChildItem.Count > 0)
        {
            foreach (Bookmark child in bm.ChildItem)
            {
                WriteBookmark(writer, child, level + 1);
            }
        }
    }
}