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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the bookmark editor and bind the loaded document
            PdfBookmarkEditor editor = new PdfBookmarkEditor();
            editor.BindPdf(doc);

            // Create a parent bookmark
            Bookmark parent = new Bookmark
            {
                Title = "Chapter 1",
                PageNumber = 1,
                Action = "GoTo"
            };

            // Create child bookmarks
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

            // Assemble the hierarchy: add children to the parent
            Bookmarks children = new Bookmarks();
            children.Add(child1);
            children.Add(child2);
            parent.ChildItem = children;

            // Add the hierarchical bookmark structure to the PDF
            editor.CreateBookmarks(parent);

            // Save the updated PDF with bookmarks
            editor.Save(outputPath);
        }

        Console.WriteLine($"Hierarchical bookmarks created: {outputPath}");
    }
}