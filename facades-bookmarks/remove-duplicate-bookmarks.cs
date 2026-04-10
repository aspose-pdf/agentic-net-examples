using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_cleaned.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the bookmark editor and bind the PDF
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPdf);

        try
        {
            // Extract all existing bookmarks
            Bookmarks allBookmarks = editor.ExtractBookmarks();

            // Deduplicate based on Title + Destination PageNumber
            var uniqueBookmarks = new List<Bookmark>();
            var seenKeys = new HashSet<string>(StringComparer.Ordinal);

            foreach (Bookmark bm in allBookmarks)
            {
                // Guard against null titles or page numbers
                string title = bm.Title ?? string.Empty;
                int pageNum = bm.PageNumber;

                string key = $"{title}|{pageNum}";
                if (!seenKeys.Contains(key))
                {
                    seenKeys.Add(key);
                    uniqueBookmarks.Add(bm);
                }
            }

            // Remove all existing bookmarks
            editor.DeleteBookmarks();

            // Re‑create only the unique bookmarks
            foreach (Bookmark bm in uniqueBookmarks)
            {
                // Ensure the title is not null before creating
                string title = bm.Title ?? string.Empty;
                editor.CreateBookmarkOfPage(title, bm.PageNumber);
            }

            // Save the cleaned PDF
            editor.Save(outputPdf);
        }
        finally
        {
            // Release resources held by the editor
            editor.Close();
        }

        Console.WriteLine($"Duplicate bookmarks removed. Output saved to '{outputPdf}'.");
    }
}