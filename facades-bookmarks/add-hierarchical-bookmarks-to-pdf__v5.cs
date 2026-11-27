using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Use PdfBookmarkEditor (not PdfContentEditor) to work with bookmarks
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            // Bind the loaded document to the editor
            bookmarkEditor.BindPdf(doc);

            // Build the bookmark hierarchy
            Bookmark parent = new Bookmark
            {
                Title      = "Chapter 5",
                PageNumber = 11,
                Level      = 1,
                Action     = "GoTo",
                Open       = true
            };

            Bookmark child1 = new Bookmark
            {
                Title      = "Section 5.1",
                PageNumber = 12,
                Level      = 2,
                Action     = "GoTo"
            };

            Bookmark child2 = new Bookmark
            {
                Title      = "Section 5.2",
                PageNumber = 13,
                Level      = 2,
                Action     = "GoTo"
            };

            // Attach children to the parent
            parent.ChildItems.Add(child1);
            parent.ChildItems.Add(child2);

            // Create the bookmarks in the PDF
            bookmarkEditor.CreateBookmarks(parent);

            // Save the modified PDF
            bookmarkEditor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarked PDF saved to '{outputPath}'.");
    }
}
