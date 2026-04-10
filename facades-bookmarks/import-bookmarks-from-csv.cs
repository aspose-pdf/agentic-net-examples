using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Facades; // PdfBookmarkEditor resides here
using Aspose.Pdf;          // Bookmark and Bookmarks classes

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";      // PDF to which bookmarks will be added
        const string csvPath        = "bookmarks.csv";  // CSV file: Title,Level,Page
        const string outputPdfPath  = "output.pdf";     // Resulting PDF with imported bookmarks

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

        // Parse CSV and build a hierarchy of Bookmark objects
        List<Bookmark> rootBookmarks = BuildBookmarkHierarchy(csvPath);

        // Apply the bookmarks to the PDF using PdfBookmarkEditor
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPdfPath);

        // Add each top‑level bookmark (which may contain nested children)
        foreach (Bookmark bm in rootBookmarks)
        {
            editor.CreateBookmarks(bm);
        }

        // Save the updated PDF
        editor.Save(outputPdfPath);
        Console.WriteLine($"Bookmarks imported and saved to '{outputPdfPath}'.");
    }

    // Reads the CSV file and returns a list of top‑level Bookmark objects
    private static List<Bookmark> BuildBookmarkHierarchy(string csvFile)
    {
        var roots = new List<Bookmark>();
        var stack = new Stack<Bookmark>(); // holds the current ancestor chain

        foreach (string rawLine in File.ReadLines(csvFile))
        {
            string line = rawLine.Trim();
            if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                continue; // skip empty lines or comments

            // Expected format: Title,Level,Page
            string[] parts = line.Split(',');
            if (parts.Length < 3)
                continue; // malformed line – ignore

            string title = parts[0].Trim();
            if (!int.TryParse(parts[1].Trim(), out int level) || level < 1)
                continue; // invalid level – ignore

            if (!int.TryParse(parts[2].Trim(), out int page) || page < 1)
                continue; // invalid page – ignore

            // Ensure the stack reflects the current level
            while (stack.Count >= level)
                stack.Pop();

            // Create the bookmark for this line
            Bookmark bm = new Bookmark
            {
                Title      = title,
                PageNumber = page
            };

            if (stack.Count == 0)
            {
                // Top‑level bookmark
                roots.Add(bm);
            }
            else
            {
                // Child of the bookmark on top of the stack
                Bookmark parent = stack.Peek();
                if (parent.ChildItem == null)
                    parent.ChildItem = new Bookmarks();

                parent.ChildItem.Add(bm);
            }

            // Push this bookmark onto the stack as a potential parent for deeper levels
            stack.Push(bm);
        }

        return roots;
    }
}