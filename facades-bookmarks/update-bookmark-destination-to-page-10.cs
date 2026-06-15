using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfBookmarkEditor, Bookmark, Bookmarks

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string bookmarkId = "MyBookmark"; // identifier of the bookmark to modify

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfBookmarkEditor (facade) to work with bookmarks.
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Load the PDF document.
            editor.BindPdf(inputPdf);

            // Extract all existing bookmarks as a Bookmarks collection.
            Bookmarks bookmarks = editor.ExtractBookmarks();

            if (bookmarks != null && bookmarks.Count > 0)
            {
                // Locate the bookmark with the specified identifier (here we treat Title as the ID).
                foreach (Bookmark bm in bookmarks)
                {
                    if (bm.Title == bookmarkId)
                    {
                        // Update the destination to page 10.
                        // The Destination property expects a string; a plain page number works.
                        bm.Destination = "10";
                        break;
                    }
                }

                // Remove all current bookmarks from the document.
                editor.DeleteBookmarks();

                // Re‑add the (now modified) bookmarks.
                foreach (Bookmark bm in bookmarks)
                {
                    editor.CreateBookmarks(bm);
                }
            }
            else
            {
                Console.WriteLine("No bookmarks found in the source PDF.");
            }

            // Save the updated PDF.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Bookmark '{bookmarkId}' updated to point to page 10. Saved as '{outputPdf}'.");
    }
}
