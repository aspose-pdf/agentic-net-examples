using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string parentTitle = "Chapter 1";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfBookmarkEditor to manipulate bookmarks
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Load the PDF file
            editor.BindPdf(inputPath);

            // Remove any existing bookmark with the same parent title
            editor.DeleteBookmarks(parentTitle);

            // Create the parent bookmark (e.g., a chapter)
            Bookmark parent = new Bookmark
            {
                Title = parentTitle,
                PageNumber = 1 // adjust to the actual start page of the chapter
            };

            // Prepare child bookmarks (e.g., sections within the chapter)
            Bookmarks children = new Bookmarks();

            Bookmark child1 = new Bookmark
            {
                Title = "Section 1.1",
                PageNumber = 2 // page where this section starts
            };
            Bookmark child2 = new Bookmark
            {
                Title = "Section 1.2",
                PageNumber = 5 // page where this section starts
            };

            // Add child bookmarks to the collection
            children.Add(child1);
            children.Add(child2);

            // Attach the child collection to the parent bookmark
            parent.ChildItems = children;

            // Insert the new bookmark hierarchy into the PDF
            editor.CreateBookmarks(parent);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks updated and saved to '{outputPath}'.");
    }
}