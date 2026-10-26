using System;
using System.IO;
using System.Drawing; // needed for System.Drawing.Color
using Aspose.Pdf;
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

        // PdfBookmarkEditor does not implement IDisposable, so no using block is required.
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

        // Load the PDF document.
        bookmarkEditor.BindPdf(inputPath);

        // Create a top‑level bookmark that points to the first page and set its color to blue.
        Bookmark firstPageBookmark = new Bookmark();
        firstPageBookmark.Title = "First Page";
        firstPageBookmark.PageNumber = 1;
        // TitleColor expects a System.Drawing.Color – fully qualify to avoid ambiguity.
        firstPageBookmark.TitleColor = System.Drawing.Color.Blue;

        // Add the bookmark to the document.
        bookmarkEditor.CreateBookmarks(firstPageBookmark);

        // Save the updated PDF.
        bookmarkEditor.Save(outputPath);

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}
