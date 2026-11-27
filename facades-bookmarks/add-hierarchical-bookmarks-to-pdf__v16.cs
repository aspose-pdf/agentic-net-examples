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

        // PdfBookmarkEditor does not have AddBookmark – use CreateBookmarkOfPage or CreateBookmarks.
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Bind the existing PDF
            editor.BindPdf(inputPath);

            // Build the bookmark hierarchy manually
            Bookmark parent = new Bookmark();
            parent.Title = "Section X";
            parent.PageNumber = 3;          // page numbers are 1‑based
            parent.Level = 0;               // top‑level bookmark

            Bookmark child = new Bookmark();
            child.Title = "Section X - Subsection";
            child.PageNumber = 4;
            child.Level = 1;                // child level

            // Attach child to parent
            parent.ChildItems.Add(child);

            // Create the bookmarks in the PDF (single Bookmark argument)
            editor.CreateBookmarks(parent);

            // Save the modified PDF
            editor.Save(outputPath);
            editor.Close();
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
