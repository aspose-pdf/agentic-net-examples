using System;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Facades; // Bookmark class lives here

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfBookmarkEditor implements IDisposable – wrap it in a using block
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Bind the existing PDF
            editor.BindPdf(inputPath);

            // ----- Build the bookmark hierarchy using the facade model -----
            // Parent (top‑level) bookmark
            Bookmark parent = new Bookmark
            {
                Title = "Parent Bookmark",
                PageNumber = 1,
                Level = 1
            };

            // First child of the parent
            Bookmark child1 = new Bookmark
            {
                Title = "Child 1",
                PageNumber = 1,
                Level = 2
            };
            // Sub‑children of child1
            Bookmark sub11 = new Bookmark
            {
                Title = "Subchild 1.1",
                PageNumber = 1,
                Level = 3
            };
            Bookmark sub12 = new Bookmark
            {
                Title = "Subchild 1.2",
                PageNumber = 2,
                Level = 3
            };
            child1.ChildItems.Add(sub11);
            child1.ChildItems.Add(sub12);

            // Second child of the parent
            Bookmark child2 = new Bookmark
            {
                Title = "Child 2",
                PageNumber = 3,
                Level = 2
            };
            // Sub‑children of child2
            Bookmark sub21 = new Bookmark
            {
                Title = "Subchild 2.1",
                PageNumber = 3,
                Level = 3
            };
            Bookmark sub22 = new Bookmark
            {
                Title = "Subchild 2.2",
                PageNumber = 4,
                Level = 3
            };
            child2.ChildItems.Add(sub21);
            child2.ChildItems.Add(sub22);

            // Attach children to the parent
            parent.ChildItems.Add(child1);
            parent.ChildItems.Add(child2);

            // Create the bookmark hierarchy in the PDF
            editor.CreateBookmarks(parent);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
