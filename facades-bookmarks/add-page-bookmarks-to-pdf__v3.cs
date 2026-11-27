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

        // Use a using block to ensure the editor is disposed properly
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            // Bind the source PDF document
            bookmarkEditor.BindPdf(inputPath);

            // Create bookmarks for pages 10‑12 (PdfBookmarkEditor does not have AddBookmark)
            bookmarkEditor.CreateBookmarkOfPage("Topic X", 10);
            bookmarkEditor.CreateBookmarkOfPage("Topic Y", 11);
            bookmarkEditor.CreateBookmarkOfPage("Topic Z", 12);

            // Save the PDF with the new bookmarks
            bookmarkEditor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
