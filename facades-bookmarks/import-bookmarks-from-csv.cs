using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;   // PdfBookmarkEditor, Bookmark, Bookmarks

class Program
{
    static void Main()
    {
        // Input PDF, CSV with bookmarks, and output PDF paths
        const string pdfPath      = "input.pdf";
        const string csvPath      = "bookmarks.csv";
        const string outputPath   = "output.pdf";

        // Verify files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV not found: {csvPath}");
            return;
        }

        // ------------------------------------------------------------
        // Parse CSV file.
        // Expected format per line: Title,Level,PageNumber
        // Example: "Chapter 1",1,5
        // ------------------------------------------------------------
        var topLevelBookmarks = new List<Bookmark>();          // holds level‑1 bookmarks
        var lastBookmarkAtLevel = new Dictionary<int, Bookmark>(); // tracks the most recent bookmark per level

        foreach (string rawLine in File.ReadAllLines(csvPath))
        {
            if (string.IsNullOrWhiteSpace(rawLine))
                continue; // skip empty lines

            string[] parts = rawLine.Split(',');
            if (parts.Length < 3)
                continue; // malformed line

            string title = parts[0].Trim();

            if (!int.TryParse(parts[1].Trim(), out int level) || level < 1)
                continue; // invalid level

            if (!int.TryParse(parts[2].Trim(), out int pageNumber) || pageNumber < 1)
                continue; // invalid page number

            // Create a new bookmark instance
            Bookmark bookmark = new Bookmark {
                Title      = title,
                PageNumber = pageNumber
            };

            if (level == 1)
            {
                // Top‑level bookmark
                topLevelBookmarks.Add(bookmark);
            }
            else
            {
                // Find the parent bookmark (level‑1)
                if (lastBookmarkAtLevel.TryGetValue(level - 1, out Bookmark parent))
                {
                    // Ensure the parent has a child collection
                    if (parent.ChildItem == null)
                        parent.ChildItem = new Bookmarks();

                    parent.ChildItem.Add(bookmark);
                }
                else
                {
                    // No parent found – treat as top‑level to avoid loss
                    topLevelBookmarks.Add(bookmark);
                }
            }

            // Update the tracker for the current level
            lastBookmarkAtLevel[level] = bookmark;

            // Remove deeper levels from the tracker (they are no longer ancestors)
            var keysToRemove = new List<int>();
            foreach (int key in lastBookmarkAtLevel.Keys)
                if (key > level)
                    keysToRemove.Add(key);
            foreach (int key in keysToRemove)
                lastBookmarkAtLevel.Remove(key);
        }

        // ------------------------------------------------------------
        // Apply the bookmarks to the PDF using PdfBookmarkEditor.
        // ------------------------------------------------------------
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Load the source PDF
            editor.BindPdf(pdfPath);

            // Add each top‑level bookmark (hierarchy is already attached)
            foreach (Bookmark bm in topLevelBookmarks)
            {
                editor.CreateBookmarks(bm);
            }

            // Save the result
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks imported successfully to '{outputPath}'.");
    }
}