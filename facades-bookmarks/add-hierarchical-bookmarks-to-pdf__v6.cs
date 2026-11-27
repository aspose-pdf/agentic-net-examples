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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create the bookmark editor (does NOT implement IDisposable)
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

        // Bind the existing PDF file
        bookmarkEditor.BindPdf(inputPath);

        // Build the bookmark hierarchy using the facade model (Bookmark class)
        Bookmark parent = new Bookmark();
        parent.Title = "Main";
        parent.PageNumber = 1;               // points to page 1
        parent.Action = "GoTo";             // optional, tells PDF to navigate

        Bookmark child2 = new Bookmark();
        child2.Title = "Chapter 2";
        child2.PageNumber = 2;
        child2.Action = "GoTo";

        Bookmark child3 = new Bookmark();
        child3.Title = "Chapter 3";
        child3.PageNumber = 3;
        child3.Action = "GoTo";

        Bookmark child4 = new Bookmark();
        child4.Title = "Chapter 4";
        child4.PageNumber = 4;
        child4.Action = "GoTo";

        // Attach children to the parent bookmark
        parent.ChildItems.Add(child2);
        parent.ChildItems.Add(child3);
        parent.ChildItems.Add(child4);

        // Add the whole hierarchy to the PDF via the editor
        bookmarkEditor.CreateBookmarks(parent);

        // Save the updated PDF
        bookmarkEditor.Save(outputPath);

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
