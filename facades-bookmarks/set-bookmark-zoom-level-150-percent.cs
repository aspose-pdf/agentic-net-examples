using System;
using System.IO;
using Aspose.Pdf;
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

        // Use PdfBookmarkEditor (facade) to work with bookmarks.
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPath);

            // Extract existing bookmarks.
            Bookmarks bookmarks = editor.ExtractBookmarks();

            // Update zoom level for every bookmark (including children).
            SetZoomLevelRecursive(bookmarks, 150);

            // Replace the old bookmarks with the modified ones.
            editor.DeleteBookmarks();               // remove all existing bookmarks
            foreach (Bookmark bm in bookmarks)
            {
                editor.CreateBookmarks(bm);        // add the updated bookmark back
            }

            // Save the PDF with the new bookmark settings.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks updated with 150% zoom. Saved to '{outputPath}'.");
    }

    // Recursively walks through a Bookmarks collection and sets the zoom (XYZ destination).
    static void SetZoomLevelRecursive(Bookmarks collection, int zoomPercent)
    {
        foreach (Bookmark bm in collection)
        {
            // Set the destination to XYZ with the required zoom.
            bm.PageDisplay = "XYZ";          // XYZ destination type
            bm.PageDisplay_Zoom = zoomPercent; // zoom in percent (e.g., 150 = 150%)

            // Process child bookmarks, if any.
            if (bm.ChildItems != null && bm.ChildItems.Count > 0)
            {
                SetZoomLevelRecursive(bm.ChildItems, zoomPercent);
            }
        }
    }
}
