using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "bookmarked_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the bookmark editor and bind the existing PDF
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            bookmarkEditor.BindPdf(inputPath);

            // Build the bookmark hierarchy using the Facade Bookmark class
            Bookmark parent = new Bookmark
            {
                Title      = "Parent Bookmark",
                PageNumber = 1,
                Level      = 1
            };

            Bookmark child1 = new Bookmark
            {
                Title      = "Child 1",
                PageNumber = 2,
                Level      = 2
            };
            Bookmark child1_1 = new Bookmark
            {
                Title      = "Child 1.1",
                PageNumber = 3,
                Level      = 3
            };
            // attach grand‑child to child1
            child1.ChildItems.Add(child1_1);

            Bookmark child2 = new Bookmark
            {
                Title      = "Child 2",
                PageNumber = 4,
                Level      = 2
            };
            Bookmark child2_1 = new Bookmark
            {
                Title      = "Child 2.1",
                PageNumber = 5,
                Level      = 3
            };
            // attach grand‑child to child2
            child2.ChildItems.Add(child2_1);

            // attach children to the parent
            parent.ChildItems.Add(child1);
            parent.ChildItems.Add(child2);

            // Add the whole hierarchy to the PDF
            bookmarkEditor.CreateBookmarks(parent);

            // Save the PDF with the new bookmark hierarchy
            bookmarkEditor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
