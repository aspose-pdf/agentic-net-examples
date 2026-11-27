using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // for the Bookmark class

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

        // PdfBookmarkEditor does not implement IDisposable, so we use a plain instance.
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

        // Bind the source PDF file.
        bookmarkEditor.BindPdf(inputPath);

        // Create a single Bookmark object for the "Preface" on page 1.
        Bookmark preface = new Bookmark
        {
            Title = "Preface",
            PageNumber = 1,
            Action = "GoTo" // required action string for a page‑go bookmark
        };

        // Add the bookmark to the document.
        bookmarkEditor.CreateBookmarks(preface);

        // Save the PDF with the new bookmark.
        bookmarkEditor.Save(outputPath);
        bookmarkEditor.Close();

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}
