using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Recursively collapse bookmarks whose title matches the specified value.
    static void CollapseBookmarks(Bookmarks bookmarks, string titleToCollapse)
    {
        foreach (Bookmark bm in bookmarks)
        {
            if (bm.Title == titleToCollapse)
                bm.Open = false; // set collapsed

            // If the bookmark has children, process them as well.
            if (bm.ChildItems != null && bm.ChildItems.Count > 0)
                CollapseBookmarks(bm.ChildItems, titleToCollapse);
        }
    }

    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_collapsed.pdf";
        const string targetTitle = "Chapter 1"; // title of bookmark to collapse

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the bookmark editor and bind the source PDF.
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPdf);

        // Extract existing bookmarks.
        Bookmarks existingBookmarks = editor.ExtractBookmarks();

        // Collapse the desired bookmark(s).
        CollapseBookmarks(existingBookmarks, targetTitle);

        // Remove all current bookmarks.
        editor.DeleteBookmarks();

        // Re‑create the (modified) bookmark hierarchy.
        foreach (Bookmark bm in existingBookmarks)
        {
            editor.CreateBookmarks(bm);
        }

        // Save the updated PDF.
        editor.Save(outputPdf);

        Console.WriteLine($"Bookmarks updated and saved to '{outputPdf}'.");
    }
}