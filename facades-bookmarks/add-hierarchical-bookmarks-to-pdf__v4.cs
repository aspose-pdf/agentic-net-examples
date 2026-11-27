using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // for Bookmark class

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_bookmarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use PdfBookmarkEditor (the Facades class that supports bookmark manipulation).
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Bind the existing PDF document.
            editor.BindPdf(inputPath);

            // Create the parent bookmark "Chapter 4" on page 9.
            Bookmark parent = new Bookmark();
            parent.Title = "Chapter 4";
            parent.PageNumber = 9;          // 1‑based page index
            parent.Level = 1;               // top‑level bookmark
            parent.Open = true;             // expanded in the outline view (optional)

            // Create the child bookmark "Section 4.1" on page 10.
            Bookmark child = new Bookmark();
            child.Title = "Section 4.1";
            child.PageNumber = 10;
            child.Level = 2;                // one level deeper than the parent
            child.Open = true;

            // Attach the child to the parent.
            parent.ChildItems.Add(child);

            // Add the bookmark hierarchy to the PDF.
            editor.CreateBookmarks(parent);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
