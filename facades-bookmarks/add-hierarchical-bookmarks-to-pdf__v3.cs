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

        // Create the bookmark editor (does not implement IDisposable)
        var bookmarkEditor = new PdfBookmarkEditor();

        // Bind the existing PDF document
        bookmarkEditor.BindPdf(inputPath);

        // Build the bookmark hierarchy using the facade Bookmark class
        var parent = new Bookmark
        {
            Title = "Parent Bookmark",
            PageNumber = 1,
            Action = "GoTo"
        };

        var child1 = new Bookmark
        {
            Title = "Child 1",
            PageNumber = 2,
            Action = "GoTo"
        };
        var subChild1 = new Bookmark
        {
            Title = "SubChild 1",
            PageNumber = 3,
            Action = "GoTo"
        };
        child1.ChildItems = new Bookmarks();
        child1.ChildItems.Add(subChild1);

        var child2 = new Bookmark
        {
            Title = "Child 2",
            PageNumber = 4,
            Action = "GoTo"
        };
        var subChild2 = new Bookmark
        {
            Title = "SubChild 2",
            PageNumber = 5,
            Action = "GoTo"
        };
        child2.ChildItems = new Bookmarks();
        child2.ChildItems.Add(subChild2);

        // Attach children to the parent bookmark
        parent.ChildItems = new Bookmarks();
        parent.ChildItems.Add(child1);
        parent.ChildItems.Add(child2);

        // Add the whole hierarchy to the PDF via CreateBookmarks
        bookmarkEditor.CreateBookmarks(parent);

        // Save the modified PDF with the new bookmark hierarchy
        bookmarkEditor.Save(outputPath);
        bookmarkEditor.Close();

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
