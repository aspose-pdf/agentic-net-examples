using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_cleaned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF for bookmark manipulation
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPath);

            // Extract the existing bookmarks (outline entries)
            Bookmarks allBookmarks = editor.ExtractBookmarks();

            // Keep track of unique (title, action) pairs
            HashSet<string> seen = new HashSet<string>(StringComparer.Ordinal);
            List<Bookmark> uniqueBookmarks = new List<Bookmark>();

            foreach (Bookmark bm in allBookmarks)
            {
                // Some bookmarks may not have an Action; treat null as empty string
                string action = bm.Action ?? string.Empty;
                string key = $"{bm.Title}|{action}";

                if (!seen.Contains(key))
                {
                    seen.Add(key);
                    uniqueBookmarks.Add(bm);
                }
            }

            // Remove all existing bookmarks
            editor.DeleteBookmarks();

            // Re‑add only the unique bookmarks
            foreach (Bookmark bm in uniqueBookmarks)
            {
                editor.CreateBookmarks(bm);
            }

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Duplicate bookmarks removed. Saved to '{outputPath}'.");
    }
}
