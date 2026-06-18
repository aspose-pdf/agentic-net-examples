using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_cleaned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the bookmark editor
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPath);

        // Extract all existing bookmarks
        Bookmarks allBookmarks = editor.ExtractBookmarks();

        // Keep only unique (Title, PageNumber) pairs
        var uniquePairs = new HashSet<(string Title, int PageNumber)>();
        var uniqueBookmarks = new List<(string Title, int PageNumber)>();

        foreach (Bookmark bm in allBookmarks)
        {
            // Some bookmarks may not have a page number (e.g., external links); skip them
            if (bm.PageNumber <= 0) continue;

            var key = (bm.Title, bm.PageNumber);
            if (uniquePairs.Add(key))
            {
                uniqueBookmarks.Add(key);
            }
        }

        // Delete all bookmarks from the document
        editor.DeleteBookmarks();

        // Re‑create only the unique bookmarks
        foreach (var (title, page) in uniqueBookmarks)
        {
            editor.CreateBookmarkOfPage(title, page);
        }

        // Save the cleaned PDF
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Duplicate bookmarks removed. Saved to '{outputPath}'.");
    }
}