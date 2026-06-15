using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_bookmarked.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Simulated database query result.
        // Replace this stub with actual DB access code (e.g., ADO.NET, Dapper, EF).
        List<(string Title, int PageNumber)> dbBookmarks = GetBookmarksFromDatabase();

        // Bind the PDF and import bookmarks.
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPdf);

            // Optional: remove existing bookmarks before adding new ones.
            // editor.DeleteBookmarks();

            foreach (var (title, page) in dbBookmarks)
            {
                // Aspose.Pdf uses 1‑based page indexing; validate page number.
                if (page < 1)
                {
                    Console.WriteLine($"Skipping invalid page number {page} for title \"{title}\"");
                    continue;
                }

                // Create a Bookmark instance for each record.
                Bookmark bm = new Bookmark
                {
                    Title = title,
                    PageNumber = page,
                    Action = "GoTo" // Navigate to the specified page.
                };

                // Add the bookmark to the document.
                editor.CreateBookmarks(bm);
            }

            // Save the modified PDF.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Bookmarks imported and saved to '{outputPdf}'.");
    }

    // Placeholder method – replace with real data access logic.
    static List<(string Title, int PageNumber)> GetBookmarksFromDatabase()
    {
        // Example static data.
        return new List<(string, int)>
        {
            ("Chapter 1", 1),
            ("Section 2.1", 5),
            ("Appendix", 12)
        };
    }
}