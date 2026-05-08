using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // PDF to analyze
        const string tocPath = "toc.txt";            // Generated TOC: each line "Title|PageNumber"

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Extract bookmarks using PdfBookmarkEditor (Facades API)
        // -----------------------------------------------------------------
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();
        bookmarkEditor.BindPdf(pdfPath);                     // Initialize facade with the PDF
        Bookmarks bookmarks = bookmarkEditor.ExtractBookmarks(); // Get all bookmark entries

        // Build a lookup: bookmark title -> page number
        var bookmarkMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        foreach (Bookmark bm in bookmarks)
        {
            // Bookmark.PageNumber holds the destination page (1‑based)
            bookmarkMap[bm.Title] = bm.PageNumber;
        }

        // -----------------------------------------------------------------
        // Load the generated Table of Contents (simple text file)
        // Format per line: Title|PageNumber
        // -----------------------------------------------------------------
        var tocMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        if (File.Exists(tocPath))
        {
            foreach (var line in File.ReadAllLines(tocPath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split('|');
                if (parts.Length == 2 && int.TryParse(parts[1].Trim(), out int page))
                {
                    tocMap[parts[0].Trim()] = page;
                }
            }
        }

        // -----------------------------------------------------------------
        // Consistency check: compare bookmark pages with TOC pages
        // -----------------------------------------------------------------
        Console.WriteLine("Bookmark vs TOC consistency check:");
        foreach (var kvp in tocMap)
        {
            string title = kvp.Key;
            int tocPage = kvp.Value;

            if (bookmarkMap.TryGetValue(title, out int bmPage))
            {
                if (bmPage == tocPage)
                {
                    Console.WriteLine($"OK: \"{title}\" page {tocPage}");
                }
                else
                {
                    Console.WriteLine($"MISMATCH: \"{title}\" TOC page {tocPage}, Bookmark page {bmPage}");
                }
            }
            else
            {
                Console.WriteLine($"MISSING BOOKMARK: \"{title}\" appears in TOC but not in bookmarks.");
            }
        }

        // Report bookmarks that have no entry in the TOC
        foreach (var title in bookmarkMap.Keys)
        {
            if (!tocMap.ContainsKey(title))
            {
                Console.WriteLine($"EXTRA BOOKMARK: \"{title}\" page {bookmarkMap[title]} not found in TOC.");
            }
        }

        // Clean up the facade
        bookmarkEditor.Close();
    }
}