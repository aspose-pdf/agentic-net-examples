using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added for TextAbsorber and related types

class BookmarkTocConsistencyChecker
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        // Initialize the bookmark editor on the same document
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor(doc))
        {
            // Extract all bookmarks (recursive)
            Bookmarks bookmarks = bookmarkEditor.ExtractBookmarks();

            // Build a dictionary of bookmark title -> page number
            var bookmarkPages = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            foreach (Bookmark bm in bookmarks)
                CollectBookmarks(bm, bookmarkPages);

            // Generate a simple Table of Contents (TOC) for the document.
            // In a real scenario this could be built by scanning headings, etc.
            // Here we use a placeholder method that returns title -> page mappings.
            Dictionary<string, int> tocPages = GenerateTableOfContents(doc);

            // Compare bookmark page numbers with TOC entries
            Console.WriteLine("Bookmark vs TOC consistency check:");
            foreach (var kvp in bookmarkPages)
            {
                string title = kvp.Key;
                int bookmarkPage = kvp.Value;

                if (tocPages.TryGetValue(title, out int tocPage))
                {
                    if (bookmarkPage == tocPage)
                        Console.WriteLine($"[OK] \"{title}\" – page {bookmarkPage}");
                    else
                        Console.WriteLine($"[MISMATCH] \"{title}\" – bookmark page {bookmarkPage}, TOC page {tocPage}");
                }
                else
                {
                    Console.WriteLine($"[MISSING IN TOC] \"{title}\" – bookmark page {bookmarkPage}");
                }
            }
        }
    }

    // Recursively collect bookmarks and their destination page numbers
    private static void CollectBookmarks(Bookmark bm, Dictionary<string, int> dict)
    {
        // Bookmark.PageNumber is 1‑based as per Aspose.Pdf conventions
        dict[bm.Title] = bm.PageNumber;

        // Process child bookmarks if any (use ChildItems instead of obsolete ChildItem)
        if (bm.ChildItems != null && bm.ChildItems.Count > 0)
        {
            foreach (Bookmark child in bm.ChildItems)
                CollectBookmarks(child, dict);
        }
    }

    // Placeholder for TOC generation.
    // Returns a dictionary of title -> page number.
    // Replace with actual TOC extraction logic as needed.
    private static Dictionary<string, int> GenerateTableOfContents(Document doc)
    {
        var toc = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        // Example: assume each page starts with a heading that matches the bookmark title.
        // Use a simple TextAbsorber to get the first line of each page.
        for (int i = 1; i <= doc.Pages.Count; i++)
        {
            Page page = doc.Pages[i];
            // Extract the first line of text on the page
            TextAbsorber absorber = new TextAbsorber();
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
            absorber.TextSearchOptions = new TextSearchOptions(true);
            absorber.Visit(page);
            string firstLine = absorber.Text?.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)[0]?.Trim();

            if (!string.IsNullOrEmpty(firstLine))
                toc[firstLine] = i;
        }

        return toc;
    }
}
