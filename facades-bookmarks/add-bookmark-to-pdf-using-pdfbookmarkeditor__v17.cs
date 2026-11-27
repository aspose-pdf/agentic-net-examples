using System;
using System.IO;
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

        // PdfBookmarkEditor does not implement IDisposable, so we instantiate it directly.
        var bookmarkEditor = new PdfBookmarkEditor();

        // Load the PDF document into the editor.
        bookmarkEditor.BindPdf(inputPath);

        // Add a bookmark titled "Dedication" that points to page 1 (1‑based index).
        // The correct method on PdfBookmarkEditor is CreateBookmarkOfPage.
        bookmarkEditor.CreateBookmarkOfPage("Dedication", 1);

        // Save the modified PDF with the new bookmark.
        bookmarkEditor.Save(outputPath);
        bookmarkEditor.Close();

        Console.WriteLine($"Bookmark added and PDF saved to '{outputPath}'.");
    }
}
