using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

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

        // Bind the PDF to the bookmark editor
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPath);

        // Extract all existing bookmarks
        Bookmarks allBookmarks = editor.ExtractBookmarks();

        // Determine unique bookmarks based on Title and PageNumber
        var seenKeys = new HashSet<string>();
        var uniqueBookmarks = new List<Bookmark>();

        foreach (Bookmark bm in allBookmarks)
        {
            // Combine title and page number to form a unique key
            string key = $"{bm.Title}|{bm.PageNumber}";
            if (!seenKeys.Contains(key))
            {
                seenKeys.Add(key);
                uniqueBookmarks.Add(bm);
            }
        }

        // Delete all bookmarks from the document
        editor.DeleteBookmarks();

        // Re‑create only the unique bookmarks
        foreach (Bookmark bm in uniqueBookmarks)
        {
            editor.CreateBookmarkOfPage(bm.Title, bm.PageNumber);
        }

        // Save the cleaned PDF
        editor.Save(outputPath);
    }
}