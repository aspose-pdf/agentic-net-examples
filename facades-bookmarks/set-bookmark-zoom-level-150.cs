using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Recursively set the zoom factor for a bookmark and all its children.
    static void SetZoom(Bookmark bookmark, int zoomPercent)
    {
        // Set the zoom factor (150% = 150).
        bookmark.PageDisplay_Zoom = zoomPercent;

        // Process child bookmarks if any.
        if (bookmark.ChildItems != null)
        {
            foreach (Bookmark child in bookmark.ChildItems)
            {
                SetZoom(child, zoomPercent);
            }
        }
    }

    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use PdfBookmarkEditor (facade) to work with bookmarks.
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Bind the existing PDF document.
            editor.BindPdf(inputPdf);

            // Extract the top‑level bookmarks collection.
            Bookmarks topBookmarks = editor.ExtractBookmarks();

            // If the document contains bookmarks, update their zoom levels.
            if (topBookmarks != null && topBookmarks.Count > 0)
            {
                foreach (Bookmark bm in topBookmarks)
                {
                    SetZoom(bm, 150); // 150% magnification
                }
            }

            // Save the modified PDF.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Bookmarks updated with 150% zoom and saved to '{outputPdf}'.");
    }
}
