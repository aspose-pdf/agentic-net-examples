using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_bookmarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // PdfBookmarkEditor does not implement IDisposable, so we manage it manually.
            var bookmarkEditor = new PdfBookmarkEditor();
            bookmarkEditor.BindPdf(inputPath);

            // Build the bookmark hierarchy using the facade Bookmark class.
            var parent = new Bookmark
            {
                Title      = "Parent Bookmark",
                PageNumber = 1,
                Open       = true
            };

            var child1 = new Bookmark
            {
                Title      = "Child 1",
                PageNumber = 2,
                Open       = true
            };
            var child1_1 = new Bookmark
            {
                Title      = "Child 1.1",
                PageNumber = 3,
                Open       = false
            };
            child1.ChildItems.Add(child1_1);

            var child2 = new Bookmark
            {
                Title      = "Child 2",
                PageNumber = 4,
                Open       = true
            };
            var child2_1 = new Bookmark
            {
                Title      = "Child 2.1",
                PageNumber = 5,
                Open       = false
            };
            child2.ChildItems.Add(child2_1);

            // Attach children to the parent.
            parent.ChildItems.Add(child1);
            parent.ChildItems.Add(child2);

            // Create the whole hierarchy in one call.
            bookmarkEditor.CreateBookmarks(parent);

            // Save the modified PDF.
            bookmarkEditor.Save(outputPath);
            bookmarkEditor.Close();

            Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
