using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_bookmarks.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Create a PdfBookmarkEditor instance, bind the source PDF,
        // add bookmarks, and save the result.
        // PdfBookmarkEditor implements IDisposable via SaveableFacade,
        // so we wrap it in a using block for deterministic cleanup.
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Load the PDF document into the editor
            editor.BindPdf(inputPdf);

            // Example 1: Create a simple bookmark for each page
            // This will generate a flat list of bookmarks named "Page 1", "Page 2", etc.
            editor.CreateBookmarks();

            // Example 2: Create a custom hierarchical bookmark structure
            // (optional – uncomment to use)
            /*
            Bookmark parent = new Bookmark
            {
                Title = "Chapter 1",
                PageNumber = 1,
                Action = "GoTo"
            };

            // Child bookmarks
            Bookmark child1 = new Bookmark
            {
                Title = "Section 1.1",
                PageNumber = 2,
                Action = "GoTo"
            };
            Bookmark child2 = new Bookmark
            {
                Title = "Section 1.2",
                PageNumber = 3,
                Action = "GoTo"
            };

            // Assemble hierarchy
            Bookmarks children = new Bookmarks();
            children.Add(child1);
            children.Add(child2);
            parent.ChildItem = children;

            // Add the hierarchical bookmark to the document
            editor.CreateBookmarks(parent);
            */

            // Save the modified PDF with the new bookmarks
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPdf}'.");
    }
}