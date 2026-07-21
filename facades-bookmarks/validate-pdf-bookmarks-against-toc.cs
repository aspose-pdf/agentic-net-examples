using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // ---------- Extract bookmarks ----------
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();
        bookmarkEditor.BindPdf(pdfPath);
        Bookmarks bookmarks = bookmarkEditor.ExtractBookmarks();

        // ---------- Load document to build a simple TOC ----------
        using (Document doc = new Document(pdfPath))
        {
            // Build a minimal table of contents based on font size heuristic
            List<TocEntry> toc = BuildSimpleToc(doc);

            // ---------- Compare bookmark page numbers with TOC ----------
            foreach (Bookmark bm in bookmarks)
            {
                // Find a TOC entry with the same title (case‑insensitive)
                TocEntry match = toc.Find(e => string.Equals(e.Title, bm.Title, StringComparison.OrdinalIgnoreCase));

                if (match != null)
                {
                    // Aspose.Pdf.Facades.Bookmark stores its destination as a string.
                    // The string often contains the page number (e.g., "page=3").
                    // We extract the first integer we can find; if none is found we treat it as undefined (-1).
                    int bookmarkPage = ExtractPageNumberFromDestination(bm.Destination);

                    if (match.PageNumber != bookmarkPage)
                    {
                        Console.WriteLine($"Mismatch: \"{bm.Title}\" – bookmark page {bookmarkPage}, TOC page {match.PageNumber}");
                    }
                }
                else
                {
                    Console.WriteLine($"Bookmark title not found in TOC: \"{bm.Title}\"");
                }
            }
        }

        // Clean up the facade
        bookmarkEditor.Close();
    }

    // Simple structure to hold TOC items
    class TocEntry
    {
        public string Title { get; set; }
        public int PageNumber { get; set; }

        public TocEntry(string title, int pageNumber)
        {
            Title = title ?? string.Empty;
            PageNumber = pageNumber;
        }
    }

    // Heuristic TOC builder: treats text fragments with font size >= 14 as headings
    static List<TocEntry> BuildSimpleToc(Document doc)
    {
        var toc = new List<TocEntry>();

        for (int pageIdx = 1; pageIdx <= doc.Pages.Count; pageIdx++)
        {
            var page = doc.Pages[pageIdx];
            var absorber = new TextFragmentAbsorber();
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
            page.Accept(absorber);

            foreach (TextFragment tf in absorber.TextFragments)
            {
                // Guard against null TextState (unlikely but safe) and null Text
                if (tf?.TextState != null && tf.TextState.FontSize >= 14 && !string.IsNullOrWhiteSpace(tf.Text))
                {
                    toc.Add(new TocEntry(tf.Text.Trim(), pageIdx));
                }
            }
        }

        return toc;
    }

    // Extracts the first integer found in a destination string.
    // Returns -1 if no integer can be parsed.
    static int ExtractPageNumberFromDestination(string destination)
    {
        if (string.IsNullOrWhiteSpace(destination))
            return -1;

        // Destination strings can be in many formats, e.g., "page=3", "3", "FitH 3", etc.
        // We split on non‑digit characters and try to parse the first token that is a number.
        var parts = System.Text.RegularExpressions.Regex.Split(destination, "\\D+");
        foreach (var part in parts)
        {
            if (int.TryParse(part, out int page))
                return page;
        }
        return -1;
    }
}
