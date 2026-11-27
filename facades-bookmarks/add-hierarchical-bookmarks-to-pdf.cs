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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfBookmarkEditor does NOT implement IDisposable; do NOT wrap in using
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

        // Bind the existing PDF to the editor
        bookmarkEditor.BindPdf(inputPath);

        // Create parent bookmark "Chapter 2" on page 5
        Bookmark parent = new Bookmark
        {
            Title = "Chapter 2",
            PageNumber = 5,          // use PageNumber, not DestinationPageNumber
            Action = "GoTo"         // required action string for a navigation bookmark
        };

        // Create child bookmark "Section 2.1" on page 6
        Bookmark child = new Bookmark
        {
            Title = "Section 2.1",
            PageNumber = 6,
            Action = "GoTo"
        };

        // Attach child to parent
        parent.ChildItems.Add(child);

        // Add the parent (with its child) to the PDF using CreateBookmarks
        bookmarkEditor.CreateBookmarks(parent);

        // Save the updated PDF
        bookmarkEditor.Save(outputPath);
        bookmarkEditor.Close();

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
