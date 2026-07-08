using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the output PDF, and the URL to open.
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string helpUrl   = "https://docs.aspose.com/pdf/net/";

        // Ensure the source file exists.
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Use PdfBookmarkEditor (a facade) to add a bookmark.
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            // Bind the existing PDF document.
            bookmarkEditor.BindPdf(inputPdf);

            // Create a bookmark that points to an external URL.
            Bookmark helpBookmark = new Bookmark
            {
                Title      = "Help",          // Visible name in the bookmarks pane.
                Action     = "URI",           // Action type for opening a web address.
                Destination = helpUrl         // The URL to open when the bookmark is selected.
            };

            // Add the bookmark to the document.
            bookmarkEditor.CreateBookmarks(helpBookmark);

            // Save the modified PDF.
            bookmarkEditor.Save(outputPdf);
        }

        Console.WriteLine($"Bookmark added and saved to '{outputPdf}'.");
    }
}