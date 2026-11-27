using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source PDF exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfBookmarkEditor is a Facades class; it does not implement IDisposable.
        var bookmarkEditor = new PdfBookmarkEditor();

        // Load (bind) the existing PDF document.
        bookmarkEditor.BindPdf(inputPath);

        // Create a bookmark titled "Glossary" that points to page 4 (1‑based indexing).
        // The correct method is CreateBookmarkOfPage, not AddBookmark.
        bookmarkEditor.CreateBookmarkOfPage("Glossary", 4);

        // Save the updated PDF with the new bookmark.
        bookmarkEditor.Save(outputPath);
        bookmarkEditor.Close();

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}
