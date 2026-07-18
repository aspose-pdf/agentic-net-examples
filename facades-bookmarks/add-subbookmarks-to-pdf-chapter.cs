using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfBookmarkEditor, Bookmark, Bookmarks

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_subbookmarks.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the bookmark editor
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPath);

            // Create child bookmarks (sub‑sections)
            Bookmark child1 = new Bookmark
            {
                Title      = "Section 1.1",
                PageNumber = 6               // destination page for this child
            };

            Bookmark child2 = new Bookmark
            {
                Title      = "Section 1.2",
                PageNumber = 8
            };

            // Collect the children in a Bookmarks collection
            Bookmarks children = new Bookmarks();
            children.Add(child1);
            children.Add(child2);

            // Create the parent bookmark (chapter) and attach the children
            Bookmark parent = new Bookmark
            {
                Title      = "Chapter 1",
                PageNumber = 5,               // destination page for the parent
                ChildItems = children          // attach child bookmarks
            };

            // Add the whole hierarchy to the document
            editor.CreateBookmarks(parent);

            // Save the updated PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks with children saved to '{outputPath}'.");
    }
}