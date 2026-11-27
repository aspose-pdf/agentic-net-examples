using System;
using System.IO;
using Aspose.Pdf.Facades;

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

        // PdfBookmarkEditor does not implement IDisposable, so we instantiate it directly.
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

        // Load (bind) the existing PDF document.
        bookmarkEditor.BindPdf(inputPath);

        // Add a bookmark titled "Foreword" that points to page 2 (pages are 1‑based).
        // Use the correct method from PdfBookmarkEditor's surface.
        bookmarkEditor.CreateBookmarkOfPage("Foreword", 2);

        // Save the PDF with the new bookmark.
        bookmarkEditor.Save(outputPath);

        // Release resources.
        bookmarkEditor.Close();

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}
