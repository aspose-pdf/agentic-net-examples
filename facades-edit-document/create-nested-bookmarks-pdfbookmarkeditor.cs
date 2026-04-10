using System;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the source PDF exists – create a simple one if it does not.
        if (!System.IO.File.Exists(inputPath))
        {
            using (Document doc = new Document())
            {
                // Add a single blank page (you can add content here if needed).
                doc.Pages.Add();
                doc.Save(inputPath);
            }
        }

        // Initialize the PdfBookmarkEditor facade
        PdfBookmarkEditor editor = new PdfBookmarkEditor();

        // Load the PDF document
        editor.BindPdf(inputPath);

        // Create child bookmarks (subsections)
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

        // Group child bookmarks
        Bookmarks childBookmarks = new Bookmarks();
        childBookmarks.Add(child1);
        childBookmarks.Add(child2);

        // Create parent bookmark (Chapter One) and attach children using the non‑obsolete property
        Bookmark parent = new Bookmark
        {
            Title = "Chapter One",
            PageNumber = 1,
            Action = "GoTo",
            // Use ChildItems instead of the obsolete ChildItem property
            ChildItems = childBookmarks
        };

        // Add the hierarchical bookmark structure to the PDF
        editor.CreateBookmarks(parent);

        // Save the updated PDF
        editor.Save(outputPath);

        Console.WriteLine($"Bookmarks added successfully. Output saved to '{outputPath}'.");
    }
}
