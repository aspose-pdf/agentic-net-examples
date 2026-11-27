using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // Bookmark and Bookmarks classes live here

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

        // Create a PdfBookmarkEditor instance and bind the PDF.
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            bookmarkEditor.BindPdf(inputPath);

            // Build the bookmark hierarchy using the Facade Bookmark model.
            // Parent bookmark "Section 1" pointing to page 2.
            Bookmark parent = new Bookmark
            {
                Title = "Section 1",
                PageNumber = 2,
                Action = "GoTo" // optional, default is GoTo
            };

            // Child bookmark "Subsection 1.1" pointing to page 3.
            Bookmark child = new Bookmark
            {
                Title = "Subsection 1.1",
                PageNumber = 3,
                Action = "GoTo"
            };

            // Attach child to parent via the ChildItems collection.
            parent.ChildItems = new Bookmarks();
            parent.ChildItems.Add(child);

            // Create the bookmark(s) in the PDF. The overload accepts a single Bookmark
            // which may contain nested ChildItems.
            bookmarkEditor.CreateBookmarks(parent);

            // Save the modified PDF.
            bookmarkEditor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
