using System;
using System.IO;
using Aspose.Pdf.Facades;          // Facade classes for bookmark editing
using Aspose.Pdf;                 // Core PDF classes (Document disposal rule)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF via PdfBookmarkEditor (facade) and bind it to the file
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();
        bookmarkEditor.BindPdf(inputPath);

        // Extract the top‑level bookmarks collection
        Bookmarks topBookmarks = bookmarkEditor.ExtractBookmarks();

        if (topBookmarks != null)
        {
            // Recursively set the zoom factor to 150% for each bookmark
            foreach (Bookmark bm in topBookmarks)
            {
                SetZoomRecursive(bm, 150);
            }
        }

        // Save the modified PDF to a new file
        bookmarkEditor.Save(outputPath);
        bookmarkEditor.Close();

        Console.WriteLine($"Bookmarks updated with 150% zoom. Saved to '{outputPath}'.");
    }

    // Helper method to set zoom on a bookmark and its children
    static void SetZoomRecursive(Bookmark bookmark, int zoomPercent)
    {
        // Set the zoom factor for the current bookmark (percentage)
        bookmark.PageDisplay_Zoom = zoomPercent;

        // Process child bookmarks, if any
        if (bookmark.ChildItems != null)
        {
            foreach (Bookmark child in bookmark.ChildItems)
            {
                SetZoomRecursive(child, zoomPercent);
            }
        }
    }
}
