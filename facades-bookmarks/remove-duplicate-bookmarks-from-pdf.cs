using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the bookmark editor and bind the PDF file
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPath);

        // Extract all existing bookmarks
        Bookmarks allBookmarks = editor.ExtractBookmarks();

        // Identify unique bookmarks based on title and destination page
        var seen = new HashSet<string>();
        var uniqueBookmarks = new List<Bookmark>();

        foreach (Bookmark bm in allBookmarks)
        {
            // Use title and page number as a composite key
            string key = $"{bm.Title}|{bm.PageNumber}";
            if (!seen.Contains(key))
            {
                seen.Add(key);
                uniqueBookmarks.Add(bm);
            }
        }

        // Delete all bookmarks from the document
        editor.DeleteBookmarks();

        // Re‑create only the unique bookmarks
        foreach (Bookmark bm in uniqueBookmarks)
        {
            string title = bm.Title ?? string.Empty;
            int page = bm.PageNumber > 0 ? bm.PageNumber : 1;
            editor.CreateBookmarkOfPage(title, page);
        }

        // Save the cleaned PDF
        editor.Save(outputPath);
    }
}