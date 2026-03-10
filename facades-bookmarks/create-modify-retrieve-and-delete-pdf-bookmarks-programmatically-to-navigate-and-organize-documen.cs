using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath = "sample.pdf";
        const string outputPath = "sample_bookmarked.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the bookmark editor facade
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

        // Load the PDF document (lifecycle: load)
        bookmarkEditor.BindPdf(inputPath);

        // -------------------------------------------------
        // 1. Create a simple bookmark for page 1
        // -------------------------------------------------
        bookmarkEditor.CreateBookmarkOfPage("Chapter 1", 1);

        // -------------------------------------------------
        // 2. Create a nested bookmark hierarchy
        // -------------------------------------------------
        // Child bookmarks
        Bookmark child1 = new Bookmark
        {
            Title = "Section 1.1",
            PageNumber = 2
        };
        Bookmark child2 = new Bookmark
        {
            Title = "Section 1.2",
            PageNumber = 3
        };

        // Parent bookmark that will contain the children
        Bookmark parent = new Bookmark
        {
            Title = "Chapter 1",
            Action = "GoTo",
            PageNumber = 1
        };

        // Attach child bookmarks to the parent
        Bookmarks children = new Bookmarks();
        children.Add(child1);
        children.Add(child2);
        parent.ChildItem = children;

        // Add the hierarchy to the PDF
        bookmarkEditor.CreateBookmarks(parent);

        // -------------------------------------------------
        // 3. Retrieve and display all bookmarks
        // -------------------------------------------------
        Bookmarks allBookmarks = bookmarkEditor.ExtractBookmarks();
        Console.WriteLine("Existing bookmarks:");
        foreach (Bookmark bm in allBookmarks)
        {
            Console.WriteLine($"- {bm.Title} (Page {bm.PageNumber})");
        }

        // -------------------------------------------------
        // 4. Modify a bookmark title
        // -------------------------------------------------
        bookmarkEditor.ModifyBookmarks("Section 1.2", "Updated Section 1.2");

        // Verify the modification
        Bookmarks modified = bookmarkEditor.ExtractBookmarks("Updated Section 1.2");
        Console.WriteLine("\nAfter modification:");
        foreach (Bookmark bm in modified)
        {
            Console.WriteLine($"- {bm.Title} (Page {bm.PageNumber})");
        }

        // -------------------------------------------------
        // 5. Delete a specific bookmark
        // -------------------------------------------------
        bookmarkEditor.DeleteBookmarks("Section 1.1");

        // -------------------------------------------------
        // 6. Delete all remaining bookmarks
        // -------------------------------------------------
        bookmarkEditor.DeleteBookmarks();

        // Save the modified PDF (lifecycle: save)
        bookmarkEditor.Save(outputPath);

        // Release resources held by the facade
        bookmarkEditor.Close();

        Console.WriteLine($"\nProcessed PDF saved to '{outputPath}'.");
    }
}