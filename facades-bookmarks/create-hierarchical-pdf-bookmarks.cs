using System;
using System.IO;
using Aspose.Pdf;
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

        // Create the bookmark editor facade
        PdfBookmarkEditor editor = new PdfBookmarkEditor();

        // Load (bind) the existing PDF document
        editor.BindPdf(inputPath);

        // -------------------------------------------------
        // Build a hierarchical bookmark structure
        // Example:
        //   Chapter 1 (page 1)
        //       Section 1.1 (page 2)
        //       Section 1.2 (page 3)
        //   Chapter 2 (page 4)
        //       Section 2.1 (page 5)
        // -------------------------------------------------

        // Child bookmarks for Chapter 1
        Bookmark sec11 = new Bookmark
        {
            Title = "Section 1.1",
            PageNumber = 2
        };
        Bookmark sec12 = new Bookmark
        {
            Title = "Section 1.2",
            PageNumber = 3
        };
        Bookmarks chapter1Children = new Bookmarks();
        chapter1Children.Add(sec11);
        chapter1Children.Add(sec12);

        Bookmark chapter1 = new Bookmark
        {
            Title = "Chapter 1",
            PageNumber = 1,
            ChildItem = chapter1Children
        };

        // Child bookmarks for Chapter 2
        Bookmark sec21 = new Bookmark
        {
            Title = "Section 2.1",
            PageNumber = 5
        };
        Bookmarks chapter2Children = new Bookmarks();
        chapter2Children.Add(sec21);

        Bookmark chapter2 = new Bookmark
        {
            Title = "Chapter 2",
            PageNumber = 4,
            ChildItem = chapter2Children
        };

        // Add top‑level bookmarks to the document
        editor.CreateBookmarks(chapter1);
        editor.CreateBookmarks(chapter2);

        // Save the modified PDF with the new bookmark hierarchy
        editor.Save(outputPath);

        // Release resources held by the facade
        editor.Close();

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}