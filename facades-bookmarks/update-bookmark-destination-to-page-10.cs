using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF containing the bookmarks
        const string inputPdf  = "input.pdf";
        // Output PDF with the updated bookmark destination
        const string outputPdf = "output_updated.pdf";
        // Identifier of the bookmark to modify – here we use the bookmark title as the ID
        const string bookmarkId = "MyBookmark";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Work with the PDF bookmarks using PdfBookmarkEditor (facade API)
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPdf);

            // Extract all existing bookmarks
            Aspose.Pdf.Facades.Bookmarks allBookmarks = editor.ExtractBookmarks();

            // Locate the bookmark that matches the supplied identifier
            Aspose.Pdf.Facades.Bookmark targetBookmark = null;
            foreach (Aspose.Pdf.Facades.Bookmark bm in allBookmarks)
            {
                if (bm.Title == bookmarkId)
                {
                    targetBookmark = bm;
                    break;
                }
            }

            if (targetBookmark == null)
            {
                Console.Error.WriteLine($"Bookmark with title '{bookmarkId}' not found.");
                // Save the original PDF unchanged
                editor.Save(outputPdf);
                return;
            }

            // Update the destination page to page 10 (Aspose.Pdf uses 1‑based indexing)
            targetBookmark.PageNumber = 10;

            // Remove all existing bookmarks from the document
            editor.DeleteBookmarks();

            // Re‑create bookmarks with the (now) updated collection
            foreach (Aspose.Pdf.Facades.Bookmark bm in allBookmarks)
            {
                // Preserve title and (possibly) updated page number
                editor.CreateBookmarkOfPage(bm.Title, bm.PageNumber);
            }

            // Persist the changes to a new file
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Bookmark '{bookmarkId}' updated to page 10 and saved to '{outputPdf}'.");
    }
}