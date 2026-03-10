using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfBookmarkEditor, Bookmark, Bookmarks

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_bookmarks.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the bookmark editor and bind the source PDF
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPdf);

            // ----- Create child bookmarks -----
            Bookmark child1 = new Bookmark
            {
                Title      = "Chapter 1 – Introduction",
                PageNumber = 2,
                Action     = "GoTo"
            };

            Bookmark child2 = new Bookmark
            {
                Title      = "Chapter 2 – Details",
                PageNumber = 5,
                Action     = "GoTo"
            };

            // Collect child bookmarks into a Bookmarks collection
            Bookmarks childBookmarks = new Bookmarks();
            childBookmarks.Add(child1);
            childBookmarks.Add(child2);

            // ----- Create parent bookmark and attach children -----
            Bookmark parent = new Bookmark
            {
                Title      = "Main Sections",
                PageNumber = 1,
                Action     = "GoTo",
                // Assign the child collection to form a hierarchy
                ChildItem  = childBookmarks
            };

            // Add the hierarchical bookmark structure to the document
            editor.CreateBookmarks(parent);

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with hierarchical bookmarks: {outputPdf}");
    }
}