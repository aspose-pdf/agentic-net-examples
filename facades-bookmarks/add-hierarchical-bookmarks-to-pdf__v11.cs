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

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfBookmarkEditor (the Facades class that supports bookmark manipulation)
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Bind the existing PDF
            editor.BindPdf(inputPath);

            // Create the parent bookmark "Chapter 6" pointing to page 12
            Bookmark parent = new Bookmark();
            parent.Title = "Chapter 6";
            parent.PageNumber = 12;          // 1‑based page index
            parent.Level = 1;                // top‑level bookmark
            parent.Action = "GoTo";         // default action

            // Create the child bookmark "Section 6.1" pointing to page 13
            Bookmark child = new Bookmark();
            child.Title = "Section 6.1";
            child.PageNumber = 13;
            child.Level = 2;                 // child level
            child.Action = "GoTo";

            // Attach child to parent
            parent.ChildItems.Add(child);

            // Add the bookmark hierarchy to the document
            editor.CreateBookmarks(parent);

            // Save the modified PDF with the new bookmarks
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
