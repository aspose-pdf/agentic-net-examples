using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BookmarkTocComparer
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // PDF to analyze
        const string tocPath = "toc.txt";            // Generated TOC (Title<TAB>PageNumber per line)

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(tocPath))
        {
            Console.Error.WriteLine($"TOC file not found: {tocPath}");
            return;
        }

        // Load PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(pdfPath))
        {
            // -----------------------------------------------------------------
            // Extract bookmarks using the Facades API
            // -----------------------------------------------------------------
            using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
            {
                bookmarkEditor.BindPdf(pdfDoc);

                // ExtractBookmarks returns a Bookmarks collection (facade model)
                Bookmarks bookmarks = bookmarkEditor.ExtractBookmarks();

                // Build a dictionary of bookmark title -> page number (1‑based)
                var bookmarkMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                foreach (Bookmark bm in bookmarks)
                {
                    // In the facade model the page number is exposed via the PageNumber property
                    int pageNum = bm.PageNumber; // 1‑based page index
                    bookmarkMap[bm.Title] = pageNum;
                }

                // -----------------------------------------------------------------
                // Load the generated Table of Contents (simple TAB‑separated file)
                // -----------------------------------------------------------------
                var tocMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                foreach (string line in File.ReadAllLines(tocPath))
                {
                    // Expected format: Title<TAB>PageNumber
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    string[] parts = line.Split('\t');
                    if (parts.Length != 2) continue;

                    string title = parts[0].Trim();
                    if (!int.TryParse(parts[1].Trim(), out int tocPage)) continue;

                    tocMap[title] = tocPage;
                }

                // -----------------------------------------------------------------
                // Compare bookmark page numbers with TOC entries
                // -----------------------------------------------------------------
                Console.WriteLine("=== Consistency Check Results ===");
                foreach (var kvp in tocMap)
                {
                    string title = kvp.Key;
                    int tocPage = kvp.Value;

                    if (bookmarkMap.TryGetValue(title, out int bmPage))
                    {
                        if (bmPage == tocPage)
                        {
                            Console.WriteLine($"OK   : \"{title}\" – page {tocPage}");
                        }
                        else
                        {
                            Console.WriteLine($"MISMATCH: \"{title}\" – TOC page {tocPage}, Bookmark page {bmPage}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"MISSING BOOKMARK: \"{title}\" – TOC page {tocPage}");
                    }
                }

                // Detect bookmarks that have no corresponding TOC entry
                foreach (var bmTitle in bookmarkMap.Keys)
                {
                    if (!tocMap.ContainsKey(bmTitle))
                    {
                        Console.WriteLine($"EXTRA BOOKMARK: \"{bmTitle}\" – Bookmark page {bookmarkMap[bmTitle]}");
                    }
                }
            }
        }
    }
}
