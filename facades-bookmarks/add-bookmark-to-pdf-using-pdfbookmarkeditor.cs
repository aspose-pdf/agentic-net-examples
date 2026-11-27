using System;
using System.IO;
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

        try
        {
            // PdfBookmarkEditor does not implement IDisposable.
            var bookmarkEditor = new PdfBookmarkEditor();

            // Load (bind) the existing PDF document.
            bookmarkEditor.BindPdf(inputPath);

            // Create a bookmark titled "Chapter 1" that points to page 5 (1‑based indexing).
            bookmarkEditor.CreateBookmarkOfPage("Chapter 1", 5);

            // Save the PDF with the new bookmark.
            bookmarkEditor.Save(outputPath);
            bookmarkEditor.Close();

            Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
