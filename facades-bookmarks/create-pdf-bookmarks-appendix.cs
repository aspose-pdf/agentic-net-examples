using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // for Bookmark class

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_bookmarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfBookmarkEditor does NOT implement IDisposable, so we instantiate it directly.
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

        // Bind the existing PDF file.
        bookmarkEditor.BindPdf(inputPath);

        // ------------------------------------------------------------
        // Option 1 – simple flat bookmarks using the dedicated method.
        // ------------------------------------------------------------
        // Create a bookmark that points to page 22.
        bookmarkEditor.CreateBookmarkOfPage("Appendix", 22);
        // Create a bookmark that points to page 24.
        bookmarkEditor.CreateBookmarkOfPage("Appendix B", 24);

        // ------------------------------------------------------------
        // Option 2 – hierarchical bookmarks using a Bookmark object.
        // (Uncomment the block below if you need a parent/child relationship.)
        // ------------------------------------------------------------
        /*
        Bookmark parent = new Bookmark
        {
            Title      = "Appendix",
            PageNumber = 22,
            Level      = 1
        };
        Bookmark child = new Bookmark
        {
            Title      = "Appendix B",
            PageNumber = 24,
            Level      = 2
        };
        // Initialise the collection for child items and add the child.
        parent.ChildItems = new Bookmarks();
        parent.ChildItems.Add(child);
        // Add the whole hierarchy in one call.
        bookmarkEditor.CreateBookmarks(parent);
        */

        // Save the modified PDF with the new bookmarks.
        bookmarkEditor.Save(outputPath);

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
