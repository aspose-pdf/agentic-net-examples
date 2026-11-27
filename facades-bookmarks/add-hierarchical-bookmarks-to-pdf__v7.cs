using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_bookmarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Instantiate the bookmark editor (does not implement IDisposable).
        var bookmarkEditor = new PdfBookmarkEditor();

        // Bind the existing PDF document.
        bookmarkEditor.BindPdf(inputPath);

        // Build the bookmark hierarchy using the facade Bookmark class.
        var parent = new Bookmark
        {
            Title = "Parent Bookmark",
            PageNumber = 1,
            Open = true // optional – expands the parent when opened
        };

        var child1 = new Bookmark { Title = "Child 1", PageNumber = 2 };
        var child2 = new Bookmark { Title = "Child 2", PageNumber = 3 };
        var child3 = new Bookmark { Title = "Child 3", PageNumber = 4 };

        // Nest children under the parent via the ChildItems collection.
        parent.ChildItems.Add(child1);
        parent.ChildItems.Add(child2);
        parent.ChildItems.Add(child3);

        // Create the bookmarks in the PDF.
        bookmarkEditor.CreateBookmarks(parent);

        // Save the updated PDF.
        bookmarkEditor.Save(outputPath);
        bookmarkEditor.Close();

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
