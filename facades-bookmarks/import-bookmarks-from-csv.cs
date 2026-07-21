using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;                 // Bookmark, Bookmarks
using Aspose.Pdf.Facades;        // PdfBookmarkEditor

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";      // PDF to which bookmarks will be added
        const string outputPdfPath = "output.pdf";     // Resulting PDF with imported bookmarks
        const string csvPath       = "bookmarks.csv"; // CSV file: Title,Level,PageNumber

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV not found: {csvPath}");
            return;
        }

        // Collection that will hold top‑level bookmarks
        Bookmarks rootBookmarks = new Bookmarks();

        // Helper list to keep track of the most recent bookmark at each level
        List<Bookmark> levelStack = new List<Bookmark>();

        // -----------------------------------------------------------------
        // Parse CSV file (expected format: Title,Level,PageNumber)
        // -----------------------------------------------------------------
        using (StreamReader reader = new StreamReader(csvPath))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue; // skip empty lines

                string[] parts = line.Split(',');
                if (parts.Length < 3)
                {
                    Console.Error.WriteLine($"Invalid line (expected 3 columns): {line}");
                    continue;
                }

                string title = parts[0].Trim();
                if (!int.TryParse(parts[1].Trim(), out int level) || level < 1)
                {
                    Console.Error.WriteLine($"Invalid level value: {parts[1]}");
                    continue;
                }

                if (!int.TryParse(parts[2].Trim(), out int pageNumber) || pageNumber < 1)
                {
                    Console.Error.WriteLine($"Invalid page number: {parts[2]}");
                    continue;
                }

                // Create a new bookmark for this entry
                Bookmark bm = new Bookmark
                {
                    Title      = title,
                    PageNumber = pageNumber,
                    Action     = "GoTo" // explicit action type
                };

                // Ensure the levelStack can hold the current level
                while (levelStack.Count < level)
                    levelStack.Add(null);

                // Attach bookmark to its parent (if any)
                if (level == 1)
                {
                    // Top‑level bookmark
                    rootBookmarks.Add(bm);
                }
                else
                {
                    Bookmark parent = levelStack[level - 2]; // parent is one level up
                    if (parent != null)
                    {
                        if (parent.ChildItem == null)
                            parent.ChildItem = new Bookmarks();

                        parent.ChildItem.Add(bm);
                    }
                    else
                    {
                        // Orphaned bookmark – treat as top level
                        rootBookmarks.Add(bm);
                    }
                }

                // Store this bookmark as the most recent at its level
                levelStack[level - 1] = bm;

                // Clear deeper levels (they are no longer ancestors)
                for (int i = level; i < levelStack.Count; i++)
                    levelStack[i] = null;
            }
        }

        // -----------------------------------------------------------------
        // Apply the constructed bookmark hierarchy to the PDF
        // -----------------------------------------------------------------
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPdfPath);

            // Add each top‑level bookmark (its children are already linked)
            foreach (Bookmark topBm in rootBookmarks)
            {
                editor.CreateBookmarks(topBm);
            }

            // Save the updated PDF
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"Bookmarks imported and saved to '{outputPdfPath}'.");
    }
}