using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string url       = "https://example.com/documentation";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the bookmark editor facade
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Load the PDF file into the facade
            editor.BindPdf(inputPdf);

            // Create a bookmark that opens an external URL
            Bookmark helpBookmark = new Bookmark
            {
                Title      = "Help",   // Bookmark name shown in the PDF viewer
                Action     = "URI",    // Action type for opening a URL
                Destination = url       // The URL to open when the bookmark is clicked
                // No color is set to avoid using System.Drawing.Color
            };

            // Add the bookmark to the document
            editor.CreateBookmarks(helpBookmark);

            // Save the updated PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Bookmark 'Help' added and saved to '{outputPdf}'.");
    }
}