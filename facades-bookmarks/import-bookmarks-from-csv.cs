using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfInputPath   = "input.pdf";
        const string csvBookmarksPath = "bookmarks.csv";
        const string pdfOutputPath  = "output.pdf";

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

        // Parse CSV and build bookmark hierarchy
        // CSV format per line: Title,Level,DestinationPage
        var topLevelBookmarks = new List<Bookmark>();
        // Keeps the most recent bookmark at each level (1‑based)
        var lastAtLevel = new Dictionary<int, Bookmark>();

        foreach (var rawLine in File.ReadLines(csvBookmarksPath))
        {
            // Skip empty lines
            if (string.IsNullOrWhiteSpace(rawLine))
                continue;

            var parts = rawLine.Split(',');
            if (parts.Length < 3)
                continue; // malformed line

            string title      = parts[0].Trim();
            if (!int.TryParse(parts[1].Trim(), out int level) || level < 1)
                continue; // invalid level
            if (!int.TryParse(parts[2].Trim(), out int pageNumber) || pageNumber < 1)
                continue; // invalid destination

            // Create bookmark for this entry
            Bookmark bm = new Bookmark {
                Title      = title,
                PageNumber = pageNumber,
                Action     = "GoTo" // explicit action for clarity
            };

            // Attach to hierarchy
            if (level == 1)
            {
                topLevelBookmarks.Add(bm);
            }
            else
            {
                // Find parent at level-1
                if (lastAtLevel.TryGetValue(level - 1, out Bookmark parent))
                {
                    // Ensure parent has a child collection
                    if (parent.ChildItem == null)
                        parent.ChildItem = new Bookmarks();

                    parent.ChildItem.Add(bm);
                }
                else
                {
                    // Orphaned entry – treat as top level
                    topLevelBookmarks.Add(bm);
                }
            }

            // Update the last bookmark seen at this level
            lastAtLevel[level] = bm;
        }

        // Apply bookmarks to the PDF using PdfBookmarkEditor
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(pdfInputPath);

        // Add each top‑level bookmark (which may contain nested children)
        foreach (var bm in topLevelBookmarks)
        {
            editor.CreateBookmarks(bm);
        }

        // Save the updated PDF
        editor.Save(pdfOutputPath);
        editor.Close();

        Console.WriteLine($"Bookmarks imported and saved to '{pdfOutputPath}'.");
    }
}