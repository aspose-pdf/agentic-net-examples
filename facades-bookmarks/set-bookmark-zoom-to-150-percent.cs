using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the bookmark editor and bind the PDF
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPath);

            // Extract all top‑level bookmarks (returns a Bookmarks collection)
            Bookmarks topBookmarks = editor.ExtractBookmarks();

            if (topBookmarks != null && topBookmarks.Count > 0)
            {
                // Set zoom factor (150%) for every bookmark recursively
                foreach (Bookmark bm in topBookmarks)
                {
                    SetZoomRecursive(bm, 150);
                }
            }

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks updated with 150% zoom saved to '{outputPath}'.");
    }

    // Recursively apply the zoom factor to a bookmark and its children
    static void SetZoomRecursive(Bookmark bookmark, int zoomPercent)
    {
        bookmark.PageDisplay_Zoom = zoomPercent;

        if (bookmark.ChildItems != null && bookmark.ChildItems.Count > 0)
        {
            foreach (Bookmark child in bookmark.ChildItems)
            {
                SetZoomRecursive(child, zoomPercent);
            }
        }
    }
}
