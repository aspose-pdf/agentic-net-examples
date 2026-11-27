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

        // PdfBookmarkEditor does not implement IDisposable, instantiate directly.
        var bookmarkEditor = new PdfBookmarkEditor();

        // Bind the source PDF document.
        bookmarkEditor.BindPdf(inputPath);

        // Create bookmarks for pages 7, 8, and 9 with the required titles.
        bookmarkEditor.CreateBookmarkOfPage("Block X", 7);
        bookmarkEditor.CreateBookmarkOfPage("Block Y", 8);
        bookmarkEditor.CreateBookmarkOfPage("Block Z", 9);

        // Save the PDF with the new bookmarks.
        bookmarkEditor.Save(outputPath);
        bookmarkEditor.Close();

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
