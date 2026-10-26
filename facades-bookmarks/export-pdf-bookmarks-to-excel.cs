using System;
using System.IO;
using Aspose.Pdf.Facades; // For bookmark extraction

class ExportBookmarksToExcel
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string csvPath = "bookmarks.csv";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Extract bookmarks from the PDF
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            bookmarkEditor.BindPdf(pdfPath);
            // In recent Aspose.Pdf versions ExtractBookmarks returns a "Bookmarks" collection
            var bookmarks = bookmarkEditor.ExtractBookmarks();

            // Write bookmarks to a CSV file (Excel can open CSV directly)
            using (var writer = new StreamWriter(csvPath))
            {
                // Header row
                writer.WriteLine("Title,Level,Destination");

                // Recursive traversal to flatten bookmarks while preserving level
                void WriteBookmarks(Bookmarks collection, int level)
                {
                    foreach (Bookmark bm in collection)
                    {
                        string title = EscapeCsv(bm.Title);
                        string destination = EscapeCsv(bm.Destination?.ToString() ?? string.Empty);
                        writer.WriteLine($"{title},{level},{destination}");

                        // Process child bookmarks, if any
                        if (bm.ChildItems != null && bm.ChildItems.Count > 0)
                        {
                            WriteBookmarks(bm.ChildItems, level + 1);
                        }
                    }
                }

                WriteBookmarks(bookmarks, 1); // Top‑level bookmarks start at level 1
            }

            Console.WriteLine($"Bookmarks exported to '{csvPath}'.");
        }
    }

    // Helper to escape CSV fields containing commas, quotes or new‑lines
    static string EscapeCsv(string value)
    {
        if (value.Contains('"') || value.Contains(',') || value.Contains('\n'))
        {
            value = value.Replace("\"", "\"\"");
            return $"\"{value}\"";
        }
        return value;
    }
}
