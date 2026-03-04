using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfBookmarkEditor, Bookmark, Bookmarks
using Aspose.Pdf;                 // Document (optional, not used directly here)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "nested_bookmarks.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // PdfBookmarkEditor implements IDisposable, so wrap it in a using block.
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Bind the existing PDF file.
            editor.BindPdf(inputPdf);

            // ----- Create child bookmarks -----
            // First child
            Bookmark child1 = new Bookmark
            {
                Title      = "Chapter 1 – Introduction",
                PageNumber = 2,               // points to page 2
                Action     = "GoTo"
            };

            // Second child
            Bookmark child2 = new Bookmark
            {
                Title      = "Chapter 2 – Details",
                PageNumber = 5,
                Action     = "GoTo"
            };

            // Collect children into a Bookmarks collection.
            Bookmarks childBookmarks = new Bookmarks();
            childBookmarks.Add(child1);
            childBookmarks.Add(child2);

            // ----- Create parent bookmark and attach children -----
            Bookmark parent = new Bookmark
            {
                Title      = "Table of Contents",
                PageNumber = 1,
                Action     = "GoTo",
                // Attach the child collection to form a nested hierarchy.
                ChildItems = childBookmarks
            };

            // Add the nested bookmark structure to the document.
            editor.CreateBookmarks(parent);

            // Save the modified PDF.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Nested bookmarks created and saved to '{outputPdf}'.");
    }
}