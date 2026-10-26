using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing; // optional, only needed if you set TitleColor etc.

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_bookmarked.pdf";
        const string parentTitle = "Chapter 1";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use the facade editor for simple bookmark operations
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            bookmarkEditor.BindPdf(inputPath);

            // Extract existing bookmarks to locate the parent and obtain its level
            Bookmarks existingBookmarks = bookmarkEditor.ExtractBookmarks();
            int parentLevel = 0;
            foreach (Bookmark bm in existingBookmarks)
            {
                if (bm.Title == parentTitle)
                {
                    parentLevel = bm.Level;
                    break;
                }
            }

            // Child bookmark definitions (title + target page)
            string[] childTitles = { "Section 1.1", "Section 1.2", "Section 1.3" };
            int[] childPages = { 2, 3, 4 };

            // Create each child bookmark one level deeper than the parent
            for (int i = 0; i < childTitles.Length; i++)
            {
                Bookmark child = new Bookmark
                {
                    Title = childTitles[i],
                    PageNumber = childPages[i],
                    Level = parentLevel + 1,
                    Action = "GoTo" // required for a navigation bookmark
                };

                // The facade API adds a single Bookmark via CreateBookmarks
                bookmarkEditor.CreateBookmarks(child);
            }

            // Persist the changes
            bookmarkEditor.Save(outputPath);
        }

        Console.WriteLine($"Child bookmarks added under '{parentTitle}'. Output saved to '{outputPath}'.");
    }
}
