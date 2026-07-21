using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades; // PdfBookmarkEditor, Bookmark, Bookmarks

class ExportBookmarksToExcel
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";
        const string excelPath = "bookmarks.xlsx"; // CSV content; Excel can open it

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Extract bookmarks from the PDF
        List<BookmarkInfo> bookmarkInfos = new List<BookmarkInfo>();
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(pdfPath);
            // Get all bookmarks (recursive)
            Bookmarks rootBookmarks = editor.ExtractBookmarks();
            // Walk the bookmark tree
            TraverseBookmarks(rootBookmarks, 1, bookmarkInfos);
        }

        // Write the data to a CSV file that Excel can open
        using (var writer = new StreamWriter(excelPath))
        {
            // Header row
            writer.WriteLine("Title,Level,Destination Page");

            // Data rows
            foreach (var info in bookmarkInfos)
            {
                // Escape commas and quotes in the title
                string safeTitle = EscapeCsvField(info.Title);
                writer.WriteLine($"{safeTitle},{info.Level},{info.DestinationPage}");
            }
        }

        Console.WriteLine($"Bookmarks exported to '{excelPath}'.");
    }

    // Helper class to hold bookmark details
    private class BookmarkInfo
    {
        public string Title { get; set; }
        public int Level { get; set; }
        public int DestinationPage { get; set; }
    }

    // Recursively traverse Aspose.Pdf.Facades.Bookmarks collection
    private static void TraverseBookmarks(Bookmarks bookmarks, int currentLevel, List<BookmarkInfo> list)
    {
        foreach (Bookmark bm in bookmarks)
        {
            // Some Bookmark objects may not have a page number (e.g., external URLs);
            // default to 0 when unavailable.
            int pageNumber = bm.PageNumber;

            list.Add(new BookmarkInfo
            {
                Title = bm.Title,
                Level = currentLevel,
                DestinationPage = pageNumber
            });

            // If the bookmark has child items, recurse with increased level
            if (bm.ChildItem != null && bm.ChildItem.Count > 0)
            {
                TraverseBookmarks(bm.ChildItem, currentLevel + 1, list);
            }
        }
    }

    // Simple CSV field escaper (handles commas, quotes and newlines)
    private static string EscapeCsvField(string field)
    {
        if (field == null)
            return string.Empty;

        bool mustQuote = field.Contains(",") || field.Contains('"') || field.Contains('\n') || field.Contains('\r');
        if (mustQuote)
        {
            string escaped = field.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }
        else
        {
            return field;
        }
    }
}
