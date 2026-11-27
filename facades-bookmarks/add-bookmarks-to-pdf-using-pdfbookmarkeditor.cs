using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use PdfBookmarkEditor (Facades) to create bookmarks – PdfContentEditor has no AddBookmark method.
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            // Bind the source PDF.
            bookmarkEditor.BindPdf(inputPath);

            // Build the bookmark hierarchy.
            Bookmark parent = new Bookmark
            {
                Title = "Chapter 10",
                PageNumber = 11,
                Level = 1
            };

            Bookmark child = new Bookmark
            {
                Title = "Section 10.1",
                PageNumber = 12,
                Level = 2
            };

            parent.ChildItems.Add(child);

            // Create the bookmarks in the document.
            bookmarkEditor.CreateBookmarks(parent);

            // Save the modified PDF.
            bookmarkEditor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
