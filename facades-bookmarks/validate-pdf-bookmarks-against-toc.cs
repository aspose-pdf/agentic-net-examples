using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Expected Table of Contents: title -> expected page number
        var expectedToc = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            { "Introduction", 1 },
            { "Chapter 1", 3 },
            { "Chapter 2", 7 }
        };

        // Extract bookmarks using PdfBookmarkEditor
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            bookmarkEditor.BindPdf(pdfPath);
            Bookmarks bookmarks = bookmarkEditor.ExtractBookmarks();

            // Iterate through all bookmarks (including nested ones)
            foreach (Bookmark bm in bookmarks)
            {
                if (expectedToc.TryGetValue(bm.Title, out int expectedPage))
                {
                    if (bm.PageNumber != expectedPage)
                    {
                        Console.WriteLine($"Mismatch: '{bm.Title}' points to page {bm.PageNumber}, expected {expectedPage}.");
                    }
                    else
                    {
                        Console.WriteLine($"OK: '{bm.Title}' correctly points to page {bm.PageNumber}.");
                    }
                }
                else
                {
                    Console.WriteLine($"Info: Bookmark '{bm.Title}' not present in expected TOC.");
                }
            }
        }
    }
}