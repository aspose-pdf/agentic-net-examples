using System;
using System.IO;
using Aspose.Pdf.Facades;          // Facade classes for bookmark manipulation
using Aspose.Pdf;                 // Core PDF types (Document, etc.)

class Program
{
    static void Main()
    {
        // Input PDF that will receive the external‑URL bookmarks
        const string inputPdf  = "source.pdf";
        // Output PDF with the newly added bookmarks
        const string outputPdf = "bookmarked.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Create the facade, bind the PDF and add bookmarks that point to external URLs
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Initialise the editor with the existing PDF file
            editor.BindPdf(inputPdf);

            // First bookmark – points to Google
            Bookmark googleBookmark = new Bookmark
            {
                Title      = "Google",
                Action     = "URI",                     // Action type for external URL
                Destination = "https://www.google.com"   // Target web address
            };
            editor.CreateBookmarks(googleBookmark);

            // Second bookmark – points to Aspose.PDF product page
            Bookmark asposeBookmark = new Bookmark
            {
                Title      = "Aspose.PDF",
                Action     = "URI",
                Destination = "https://products.aspose.com/pdf/net/"
            };
            editor.CreateBookmarks(asposeBookmark);

            // Save the modified PDF to a new file
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPdf}'.");
    }
}