using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class BookmarkTocConsistencyChecker
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (using block ensures proper disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // -----------------------------------------------------------------
            // 1. Extract bookmarks using PdfBookmarkEditor (Facades API)
            // -----------------------------------------------------------------
            PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();
            bookmarkEditor.BindPdf(inputPdfPath); // Bind the PDF file

            // Extract all bookmarks (recursive)
            Bookmarks bookmarks = bookmarkEditor.ExtractBookmarks();

            // Store bookmark titles and their destination page numbers
            var bookmarkMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            foreach (Bookmark bm in bookmarks)
            {
                // Some bookmarks may not have a page number (e.g., external links); skip those
                if (bm.PageNumber > 0)
                {
                    bookmarkMap[bm.Title] = bm.PageNumber;
                }
            }

            // -----------------------------------------------------------------
            // 2. Generate a simple Table of Contents (TOC) by scanning page text
            //    for lines that look like headings (e.g., start with a number or "Chapter")
            // -----------------------------------------------------------------
            var tocMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                // Extract raw text from the current page
                TextAbsorber absorber = new TextAbsorber();
                pdfDoc.Pages[pageNum].Accept(absorber);
                string pageText = absorber.Text ?? string.Empty;

                // Split into lines and look for simple heading patterns
                string[] lines = pageText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string line in lines)
                {
                    string trimmed = line.Trim();

                    // Very naive heading detection: starts with a number or the word "Chapter"
                    bool isHeading = false;
                    if (trimmed.Length > 0 && char.IsDigit(trimmed[0]))
                        isHeading = true;
                    else if (trimmed.StartsWith("Chapter", StringComparison.OrdinalIgnoreCase))
                        isHeading = true;

                    if (isHeading && !tocMap.ContainsKey(trimmed))
                    {
                        // Assume the line itself is the TOC entry title
                        tocMap[trimmed] = pageNum;
                    }
                }
            }

            // -----------------------------------------------------------------
            // 3. Compare bookmark page numbers with generated TOC page numbers
            // -----------------------------------------------------------------
            Console.WriteLine("Bookmark vs. TOC consistency check:");
            foreach (var kvp in bookmarkMap)
            {
                string title = kvp.Key;
                int bookmarkPage = kvp.Value;

                if (tocMap.TryGetValue(title, out int tocPage))
                {
                    if (bookmarkPage == tocPage)
                    {
                        Console.WriteLine($"[OK] \"{title}\" – page {bookmarkPage}");
                    }
                    else
                    {
                        Console.WriteLine($"[MISMATCH] \"{title}\" – bookmark page {bookmarkPage}, TOC page {tocPage}");
                    }
                }
                else
                {
                    Console.WriteLine($"[MISSING IN TOC] \"{title}\" – bookmark page {bookmarkPage}");
                }
            }

            // Also report TOC entries that have no corresponding bookmark
            foreach (var kvp in tocMap)
            {
                if (!bookmarkMap.ContainsKey(kvp.Key))
                {
                    Console.WriteLine($"[MISSING BOOKMARK] \"{kvp.Key}\" – TOC page {kvp.Value}");
                }
            }

            // No need to save the document; the editor will be disposed automatically when the method ends
        }
    }
}