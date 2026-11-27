using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // Bookmark class lives in the core namespace

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

        // PdfBookmarkEditor must be disposed (or used in a using block).
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Bind the source PDF.
            editor.BindPdf(inputPath);

            // Create three bookmark objects – one for each page.
            Bookmark bm1 = new Bookmark { Title = "Chapter I",   PageNumber = 9,  Action = "GoTo" };
            Bookmark bm2 = new Bookmark { Title = "Chapter II",  PageNumber = 10, Action = "GoTo" };
            Bookmark bm3 = new Bookmark { Title = "Chapter III", PageNumber = 11, Action = "GoTo" };

            // Add the bookmarks to the document.  The facade API uses CreateBookmarks(Bookmark).
            editor.CreateBookmarks(bm1);
            editor.CreateBookmarks(bm2);
            editor.CreateBookmarks(bm3);

            // Save the updated PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
