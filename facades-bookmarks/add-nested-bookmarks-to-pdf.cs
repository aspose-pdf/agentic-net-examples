using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Facades; // Bookmark resides in this namespace
using System.Drawing; // for TitleColor if needed

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

        // PdfBookmarkEditor does not implement IDisposable, so we use a plain instance
        var editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPath);

        // Create the top‑level bookmark (Volume 1) pointing to page 1
        Bookmark volume1 = new Bookmark
        {
            Title = "Volume 1",
            PageNumber = 1,
            Action = "GoTo",
            Open = true // expand by default (optional)
        };

        // Create a child bookmark (Chapter 1) pointing to page 2
        Bookmark chapter1 = new Bookmark
        {
            Title = "Chapter 1",
            PageNumber = 2,
            Action = "GoTo"
        };

        // Create a sub‑child bookmark (Section 1.1) pointing to page 3
        Bookmark section11 = new Bookmark
        {
            Title = "Section 1.1",
            PageNumber = 3,
            Action = "GoTo"
        };

        // Build the hierarchy using the ChildItems collection
        chapter1.ChildItems.Add(section11);
        volume1.ChildItems.Add(chapter1);

        // Add the hierarchy to the PDF – CreateBookmarks accepts a single Bookmark
        editor.CreateBookmarks(volume1);

        // Save the modified PDF
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
