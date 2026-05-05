using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;               // Bookmark class
using Aspose.Pdf.Facades;      // PdfBookmarkEditor

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF file to the bookmark editor
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPath);

        // Extract all existing bookmarks
        Bookmarks allBookmarks = editor.ExtractBookmarks();

        // Collect unique (title, page) pairs
        var uniqueBookmarks = new List<(string Title, int Page)>();
        var seen = new HashSet<string>(); // key = "title|page"

        foreach (Bookmark bm in allBookmarks)
        {
            if (string.IsNullOrEmpty(bm.Title))
                continue; // skip malformed entries

            int page = bm.PageNumber; // 0 if not set
            string key = $"{bm.Title}|{page}";

            if (!seen.Contains(key))
            {
                seen.Add(key);
                uniqueBookmarks.Add((bm.Title, page));
            }
        }

        // Delete all bookmarks from the document
        editor.DeleteBookmarks();

        // Re‑create only the unique bookmarks
        foreach (var (title, page) in uniqueBookmarks)
        {
            if (page > 0) // PdfBookmarkEditor expects a valid 1‑based page number
                editor.CreateBookmarkOfPage(title, page);
        }

        // Save the cleaned PDF
        editor.Save(outputPath);

        Console.WriteLine($"Duplicate bookmarks removed. Saved to '{outputPath}'.");
    }
}