using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

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

        try
        {
            // CREATE – instantiate the facade
            PdfBookmarkEditor editor = new PdfBookmarkEditor();

            // LOAD – bind the PDF document
            editor.BindPdf(inputPath);

            // EXTRACT existing bookmarks
            Bookmarks allBookmarks = editor.ExtractBookmarks();

            // Identify unique bookmarks (by title + page number)
            var unique = new List<Aspose.Pdf.Facades.Bookmark>();
            var seen = new HashSet<string>(); // composite key: title|page

            foreach (Aspose.Pdf.Facades.Bookmark bm in allBookmarks)
            {
                string key = $"{bm.Title}|{bm.PageNumber}";
                if (!seen.Contains(key))
                {
                    seen.Add(key);
                    unique.Add(bm);
                }
            }

            // DELETE all bookmarks
            editor.DeleteBookmarks();

            // RE‑ADD only the unique bookmarks
            foreach (Aspose.Pdf.Facades.Bookmark bm in unique)
            {
                // CreateBookmarkOfPage adds a single bookmark for the given page
                editor.CreateBookmarkOfPage(bm.Title, bm.PageNumber);
            }

            // SAVE the modified PDF
            editor.Save(outputPath);

            // OPTIONAL: release resources held by the facade
            editor.Close();

            Console.WriteLine($"Duplicate bookmarks removed. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}