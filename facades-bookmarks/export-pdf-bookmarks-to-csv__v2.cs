using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfBookmarkEditor, Bookmark, Bookmarks

class ExportBookmarksToExcel
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF with bookmarks
        const string outputCsv = "bookmarks.csv";  // Excel‑compatible CSV file

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF and extract its bookmarks
            using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
            {
                editor.BindPdf(inputPdf);
                Bookmarks rootBookmarks = editor.ExtractBookmarks();

                // Write bookmarks to CSV (Excel can open CSV directly)
                using (StreamWriter writer = new StreamWriter(outputCsv, false, System.Text.Encoding.UTF8))
                {
                    writer.WriteLine("Title,Level,Destination"); // header row
                    WriteBookmarksRecursive(rootBookmarks, 1, writer);
                }
            }

            Console.WriteLine($"Bookmarks exported to '{outputCsv}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Recursively writes each bookmark with its hierarchy level.
    private static void WriteBookmarksRecursive(Bookmarks bookmarks, int level, StreamWriter writer)
    {
        foreach (Bookmark bm in bookmarks)
        {
            // Escape title for CSV (wrap in double quotes and double any internal quotes)
            string safeTitle = $"\"{bm.Title?.Replace("\"", "\"\"")}\"";

            // Destination is the page number the bookmark points to (0 if not set)
            int destination = bm.PageNumber;

            writer.WriteLine($"{safeTitle},{level},{destination}");

            // If the bookmark has child items, process them with increased level
            if (bm.ChildItem != null && bm.ChildItem.Count > 0)
            {
                WriteBookmarksRecursive(bm.ChildItem, level + 1, writer);
            }
        }
    }
}