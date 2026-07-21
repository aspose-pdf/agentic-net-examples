using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_zoom.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the bookmark editor
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPath);

        // Extract the bookmarks collection (not an array)
        Bookmarks bookmarks = editor.ExtractBookmarks();

        // Update each bookmark (including nested ones) to open at 150% zoom
        if (bookmarks != null)
        {
            SetZoomRecursive(bookmarks, 150);
        }

        // Save the modified PDF
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Bookmarks updated with 150% zoom. Saved to '{outputPath}'.");
    }

    // Recursively set the zoom level for a collection of bookmarks
    private static void SetZoomRecursive(Bookmarks collection, int zoomPercent)
    {
        foreach (Bookmark bm in collection)
        {
            bm.PageDisplay_Zoom = zoomPercent; // 150% magnification
            // If the bookmark has child items, apply the same zoom to them
            if (bm.ChildItems != null && bm.ChildItems.Count > 0)
            {
                SetZoomRecursive(bm.ChildItems, zoomPercent);
            }
        }
    }
}