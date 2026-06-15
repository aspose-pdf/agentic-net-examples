using System;
using System.IO;
using Aspose.Pdf.Facades; // Bookmark and PdfBookmarkEditor are defined here

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the PDF to the bookmark editor
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPdf);

        // Extract the existing bookmarks as a collection (Bookmarks implements IEnumerable<Bookmark>)
        Bookmarks topBookmarks = editor.ExtractBookmarks();

        if (topBookmarks != null)
        {
            // Set the desired zoom (150 % = 150) for every bookmark recursively
            foreach (Bookmark bm in topBookmarks)
            {
                SetZoomRecursive(bm);
            }
        }

        // Remove the old bookmarks and re‑create them with the updated zoom settings
        editor.DeleteBookmarks();
        if (topBookmarks != null)
        {
            foreach (Bookmark bm in topBookmarks)
            {
                editor.CreateBookmarks(bm);
            }
        }

        // Save the modified PDF
        editor.Save(outputPdf);
        editor.Close();

        Console.WriteLine($"Bookmarks updated with 150% zoom and saved to '{outputPdf}'.");
    }

    // Recursively set the PageDisplay_Zoom property for a bookmark and all its children
    static void SetZoomRecursive(Bookmark bookmark)
    {
        // 150 corresponds to 150 % magnification
        bookmark.PageDisplay_Zoom = 150;

        if (bookmark.ChildItems != null)
        {
            foreach (Bookmark child in bookmark.ChildItems)
            {
                SetZoomRecursive(child);
            }
        }
    }
}