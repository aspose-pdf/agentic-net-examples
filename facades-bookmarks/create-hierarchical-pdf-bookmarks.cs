using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "bookmarked_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Example chapter titles with their starting page numbers.
        var chapters = new (string Title, int Page)[]
        {
            ("Chapter 1: Introduction", 1),
            ("Chapter 2: Getting Started", 5),
            ("Chapter 3: Advanced Topics", 12)
        };

        // Bind the PDF to the bookmark editor and create hierarchical bookmarks.
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            bookmarkEditor.BindPdf(inputPath);

            // Root bookmark (optional top‑level entry).
            Bookmark root = new Bookmark
            {
                Title = "Document Outline",
                Action = "GoTo" // required for a functional bookmark
            };
            // Initialise the collection that will hold child bookmarks.
            root.ChildItems = new Bookmarks();

            // Add each chapter as a child of the root bookmark.
            foreach (var (title, page) in chapters)
            {
                Bookmark chapterBookmark = new Bookmark
                {
                    Title = title,
                    PageNumber = page,
                    Action = "GoTo"
                };
                root.ChildItems.Add(chapterBookmark);
            }

            // Attach the bookmark hierarchy to the PDF.
            bookmarkEditor.CreateBookmarks(root);

            // Save the PDF with the new bookmarks.
            bookmarkEditor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
