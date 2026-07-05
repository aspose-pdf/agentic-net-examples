using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string targetTitle = "MyBookmark"; // Title of the bookmark to modify
        const int newPage = 10;                    // Destination page number

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF and work with its bookmarks using PdfBookmarkEditor
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPdf);

            // Extract all existing bookmarks (returns a Bookmarks collection)
            Bookmarks allBookmarks = editor.ExtractBookmarks();

            // Find the bookmark with the specified title (Aspose does not expose an Id property)
            Bookmark? targetBookmark = null;
            foreach (Bookmark bm in allBookmarks)
            {
                if (bm.Title == targetTitle)
                {
                    targetBookmark = bm;
                    break;
                }
            }

            if (targetBookmark == null)
            {
                Console.Error.WriteLine($"Bookmark with title '{targetTitle}' not found.");
                return;
            }

            // Update the destination to point to page 10.
            // For a simple "GoTo" action the Destination string can be "10 Fit" (page number + view mode).
            targetBookmark.Destination = $"{newPage} Fit";

            // Remove all existing bookmarks and add the updated bookmark hierarchy.
            editor.DeleteBookmarks();
            // CreateBookmarks expects a single Bookmark (the root of the hierarchy).
            editor.CreateBookmarks(targetBookmark);

            // Save the updated PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Bookmark '{targetTitle}' now points to page {newPage}. Saved as '{outputPdf}'.");
    }
}
