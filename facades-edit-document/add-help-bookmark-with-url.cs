using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_help_bookmark.pdf";
        const string helpUrl   = "https://example.com/documentation";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use PdfBookmarkEditor (facade) to add a bookmark that opens an external URL.
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Bind the existing PDF file.
            editor.BindPdf(inputPdf);

            // Create a bookmark instance.
            Bookmark helpBookmark = new Bookmark
            {
                Title    = "Help",          // Visible name in the bookmarks pane.
                Action   = "URI",           // Action type for opening a web address.
                Destination = helpUrl       // The URL to open when the bookmark is clicked.
            };

            // Add the bookmark to the document.
            editor.CreateBookmarks(helpBookmark);

            // Save the modified PDF.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Bookmark added and saved to '{outputPdf}'.");
    }
}