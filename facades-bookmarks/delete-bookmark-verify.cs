using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string bookmarkTitle = "Sample Bookmark";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Extract bookmarks before deletion
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPath);
            Bookmarks beforeBookmarks = editor.ExtractBookmarks();
            Console.WriteLine("Bookmarks before deletion:");
            foreach (Bookmark bm in beforeBookmarks)
            {
                Console.WriteLine("- " + bm.Title);
            }

            // Delete the bookmark with the specified title
            editor.DeleteBookmarks(bookmarkTitle);
            editor.Save(outputPath);
        }

        // Extract bookmarks after deletion to verify
        using (PdfBookmarkEditor verifier = new PdfBookmarkEditor())
        {
            verifier.BindPdf(outputPath);
            Bookmarks afterBookmarks = verifier.ExtractBookmarks();
            Console.WriteLine("Bookmarks after deletion:");
            foreach (Bookmark bm in afterBookmarks)
            {
                Console.WriteLine("- " + bm.Title);
            }
        }
    }
}