using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_zoomed.pdf";
        const int zoomLevel = 150; // 150% magnification

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Create the bookmark editor (lifecycle: create)
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            // Load the PDF into the editor (lifecycle: load)
            bookmarkEditor.BindPdf(inputPdf);

            // Extract the top‑level bookmarks collection (ExtractBookmarks returns Bookmarks, not a single Bookmark)
            Bookmarks topBookmarks = bookmarkEditor.ExtractBookmarks();

            if (topBookmarks != null)
            {
                // Iterate over each top‑level bookmark and set the zoom recursively
                foreach (Bookmark bm in topBookmarks)
                {
                    SetZoomForBookmarkTree(bm, zoomLevel);
                }
            }

            // Save the modified PDF (lifecycle: save)
            bookmarkEditor.Save(outputPdf);
        }

        Console.WriteLine($"Bookmarks updated with {zoomLevel}% zoom. Saved to '{outputPdf}'.");
    }

    // Recursively traverses bookmark tree and sets PageDisplay_Zoom
    private static void SetZoomForBookmarkTree(Bookmark bookmark, int zoom)
    {
        if (bookmark == null) return;

        // Set zoom factor for this bookmark
        bookmark.PageDisplay_Zoom = zoom;

        // Process child bookmarks if any
        if (bookmark.ChildItems != null)
        {
            foreach (Bookmark child in bookmark.ChildItems)
            {
                SetZoomForBookmarkTree(child, zoom);
            }
        }
    }
}