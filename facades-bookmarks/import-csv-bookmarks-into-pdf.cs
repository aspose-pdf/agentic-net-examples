using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;               // Bookmark class
using Aspose.Pdf.Facades;      // PdfBookmarkEditor

class Program
{
    static void Main()
    {
        const string pdfInputPath   = "input.pdf";
        const string csvBookmarksPath = "bookmarks.csv";
        const string pdfOutputPath  = "output_with_bookmarks.pdf";

        if (!File.Exists(pdfInputPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfInputPath}");
            return;
        }
        if (!File.Exists(csvBookmarksPath))
        {
            Console.Error.WriteLine($"CSV not found: {csvBookmarksPath}");
            return;
        }

        // Parse CSV lines: title, level, destinationPage
        // Expected format per line: Title,Level,PageNumber
        var topLevelBookmarks = new List<Bookmark>();
        // Assuming maximum nesting depth is reasonable; adjust if needed.
        const int maxDepth = 10;
        Bookmark[] lastAtLevel = new Bookmark[maxDepth + 1];

        foreach (string rawLine in File.ReadAllLines(csvBookmarksPath))
        {
            // Skip empty lines
            if (string.IsNullOrWhiteSpace(rawLine))
                continue;

            // Optional: skip header line if it contains non‑numeric level
            string[] parts = rawLine.Split(',');
            if (parts.Length < 3)
                continue; // malformed line

            string title = parts[0].Trim();
            if (!int.TryParse(parts[1].Trim(), out int level))
                continue; // cannot parse level, likely a header row
            if (!int.TryParse(parts[2].Trim(), out int pageNumber))
                continue; // cannot parse page number

            // Clamp level to supported range
            if (level < 1) level = 1;
            if (level > maxDepth) level = maxDepth;

            // Create bookmark for this entry
            Bookmark bm = new Bookmark
            {
                Title = title,
                PageNumber = pageNumber,
                Action = "GoTo" // explicit action, optional
            };

            if (level == 1)
            {
                // Top‑level bookmark
                topLevelBookmarks.Add(bm);
            }
            else
            {
                // Attach as child of the most recent bookmark at level‑1
                Bookmark parent = lastAtLevel[level - 1];
                if (parent != null)
                {
                    if (parent.ChildItem == null)
                        parent.ChildItem = new Bookmarks();

                    parent.ChildItem.Add(bm);
                }
                else
                {
                    // No parent found; treat as top‑level to avoid loss
                    topLevelBookmarks.Add(bm);
                }
            }

            // Clear deeper level references and store current bookmark
            for (int i = level; i <= maxDepth; i++)
                lastAtLevel[i] = null;
            lastAtLevel[level] = bm;
        }

        // Apply bookmarks to the PDF using PdfBookmarkEditor
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(pdfInputPath);

        foreach (Bookmark bm in topLevelBookmarks)
        {
            editor.CreateBookmarks(bm);
        }

        editor.Save(pdfOutputPath);
        editor.Close(); // optional cleanup

        Console.WriteLine($"Bookmarks imported and saved to '{pdfOutputPath}'.");
    }
}