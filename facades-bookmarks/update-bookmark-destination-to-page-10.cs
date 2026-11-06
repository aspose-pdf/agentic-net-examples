using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfBookmarkEditor

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";   // source PDF
        const string outputPath = "output.pdf"; // destination PDF
        const string bookmarkId = "bookmark123"; // title of the bookmark to modify

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the bookmark editor (Facades API)
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPath);

            // Remove any existing bookmark with the same title (Aspose.Pdf identifies bookmarks by title)
            // DeleteBookmarks(string title) deletes a bookmark whose title matches the supplied string.
            editor.DeleteBookmarks(bookmarkId);

            // Create a new bookmark that points to page 10 (1‑based indexing)
            // CreateBookmarkOfPage(string title, int pageNumber) adds a simple "GoTo" bookmark.
            editor.CreateBookmarkOfPage(bookmarkId, 10);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmark '{bookmarkId}' updated to page 10 and saved to '{outputPath}'.");
    }
}
