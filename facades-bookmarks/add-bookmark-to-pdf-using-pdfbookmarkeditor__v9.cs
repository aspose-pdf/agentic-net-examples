using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_bookmark.pdf";
        const string bookmarkTitle = "Conclusion";
        const int    bookmarkPage  = 20; // page numbers are 1‑based

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfBookmarkEditor does NOT implement IDisposable, so we manage it manually.
            PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

            // Bind the existing PDF file.
            bookmarkEditor.BindPdf(inputPath);

            // Create a facade Bookmark for the desired page.
            Bookmark bm = new Bookmark();
            bm.Title      = bookmarkTitle;
            bm.PageNumber = bookmarkPage;   // 1‑based page number
            bm.Action     = "GoTo";        // required action string for a page link

            // Add the bookmark to the document.
            bookmarkEditor.CreateBookmarks(bm);

            // Save the modified PDF to a new file.
            bookmarkEditor.Save(outputPath);
            bookmarkEditor.Close();

            Console.WriteLine($"Bookmark \"{bookmarkTitle}\" added on page {bookmarkPage}.");
            Console.WriteLine($"Saved updated PDF to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
