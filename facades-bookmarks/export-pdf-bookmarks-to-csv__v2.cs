using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfBookmarkEditor, Bookmark, Bookmarks

class ExportBookmarksToCsv
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "bookmarks.csv"; // Excel can open CSV files

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Initialize the bookmark editor and bind the PDF file
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();
        bookmarkEditor.BindPdf(inputPdfPath);

        // Extract all bookmarks (recursive hierarchy)
        Bookmarks allBookmarks = bookmarkEditor.ExtractBookmarks();

        // Create a CSV writer (Excel‑compatible)
        using (var writer = new StreamWriter(outputCsvPath))
        {
            // Write header row
            writer.WriteLine("Title,Level,Destination Page");

            // Row counter is not needed for CSV; we just write lines sequentially

            // Recursive traversal to write bookmarks
            void WriteBookmarks(Bookmarks bookmarks, int level)
            {
                foreach (Bookmark bm in bookmarks)
                {
                    // Escape commas in the title by surrounding with double quotes if needed
                    string title = bm.Title ?? string.Empty;
                    if (title.Contains(",") || title.Contains('"'))
                    {
                        title = "\"" + title.Replace("\"", "\"\"") + "\"";
                    }

                    // Destination page may be -1 if not set; write empty string in that case
                    string page = bm.PageNumber > 0 ? bm.PageNumber.ToString() : string.Empty;

                    writer.WriteLine($"{title},{level},{page}");

                    // Process child bookmarks, if any
                    if (bm.ChildItem != null && bm.ChildItem.Count > 0)
                    {
                        WriteBookmarks(bm.ChildItem, level + 1);
                    }
                }
            }

            // Start recursion at level 1 (top‑level bookmarks)
            WriteBookmarks(allBookmarks, 1);
        }

        // Clean up
        bookmarkEditor.Close();

        Console.WriteLine($"Bookmarks exported to '{outputCsvPath}'.");
    }
}
