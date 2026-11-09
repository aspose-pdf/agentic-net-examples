using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "bookmarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the existing PDF to the PdfBookmarkEditor (the Facade class that handles bookmarks)
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPath);

            // ----- Build the bookmark hierarchy -----
            // Root bookmark – Chapter One (page 2)
            Bookmark root = new Bookmark
            {
                Title = "Chapter One",
                PageNumber = 2,
                Action = "GoTo" // Facade model expects a string action name
            };

            // Sub‑section bookmarks (pages 3, 5, 7)
            Bookmark sec1 = new Bookmark { Title = "Section 1.1", PageNumber = 3, Action = "GoTo" };
            Bookmark sec2 = new Bookmark { Title = "Section 1.2", PageNumber = 5, Action = "GoTo" };
            Bookmark sec3 = new Bookmark { Title = "Section 1.3", PageNumber = 7, Action = "GoTo" };

            // Attach children to the root bookmark
            Bookmarks children = new Bookmarks();
            children.Add(sec1);
            children.Add(sec2);
            children.Add(sec3);
            root.ChildItems = children;

            // Create the bookmark structure in the PDF (CreateBookmarks expects a single Bookmark)
            editor.CreateBookmarks(root);

            // Save the updated PDF
            editor.Save(outputPath);
        }
    }
}
