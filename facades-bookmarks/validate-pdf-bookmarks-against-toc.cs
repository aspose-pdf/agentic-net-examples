using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // ---------- Extract bookmarks ----------
        var bookmarks = new List<(string Title, int Page)>();
        using (PdfBookmarkEditor bmEditor = new PdfBookmarkEditor())
        {
            bmEditor.BindPdf(inputPath);
            Bookmarks bms = bmEditor.ExtractBookmarks();
            foreach (Bookmark bm in bms)
            {
                // PageNumber is 1‑based
                bookmarks.Add((bm.Title, bm.PageNumber));
            }
        }

        // ---------- Generate a simple TOC ----------
        // Instead of using TextFragment (which does not expose a PageNumber property),
        // we walk through the document pages and collect Heading objects.  A Heading
        // represents a TOC‑like entry and its page can be obtained from the Page
        // that contains it.
        var toc = new List<(string Title, int Page)>();
        using (Document doc = new Document(inputPath))
        {
            foreach (Page page in doc.Pages)
            {
                foreach (var paragraph in page.Paragraphs)
                {
                    if (paragraph is Heading heading)
                    {
                        // Build the title from the heading's text segments.
                        string title = string.Empty;
                        if (heading.Segments != null && heading.Segments.Count > 0)
                        {
                            title = string.Join("", heading.Segments.Select(s => s.Text));
                        }
                        toc.Add((title.Trim(), page.Number));
                    }
                }
            }
        }

        // ---------- Compare bookmarks with TOC ----------
        Console.WriteLine("Bookmark vs TOC consistency check:");
        foreach (var bm in bookmarks)
        {
            // Find TOC entry with matching title (case‑insensitive)
            var match = toc.FirstOrDefault(t =>
                string.Equals(t.Title.Trim(), bm.Title.Trim(), StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(match.Title))
            {
                if (match.Page == bm.Page)
                {
                    Console.WriteLine($"OK: \"{bm.Title}\" page {bm.Page}");
                }
                else
                {
                    Console.WriteLine($"Mismatch: \"{bm.Title}\" bookmark page {bm.Page}, TOC page {match.Page}");
                }
            }
            else
            {
                Console.WriteLine($"Missing in TOC: \"{bm.Title}\" (bookmark page {bm.Page})");
            }
        }
    }
}
