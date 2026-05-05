using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Facades; // PdfBookmarkEditor resides here

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_collapsed.pdf";

        // Titles of bookmarks that should be collapsed (closed)
        string[] bookmarksToCollapse = { "Chapter 1", "Appendix" };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the bookmark editor and bind the source PDF
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPdf);

        // Extract all bookmarks from the document
        Aspose.Pdf.Facades.Bookmarks allBookmarks = editor.ExtractBookmarks();

        // Recursively set the Open property to false for matching titles
        SetBookmarksOpenState(allBookmarks, bookmarksToCollapse, false);

        // Save the modified PDF
        editor.Save(outputPdf);

        Console.WriteLine($"Bookmarks collapsed and saved to '{outputPdf}'.");
    }

    // Recursive helper to walk through the bookmark hierarchy
    static void SetBookmarksOpenState(Aspose.Pdf.Facades.Bookmarks bookmarks, string[] titles, bool openState)
    {
        foreach (Aspose.Pdf.Facades.Bookmark bm in bookmarks)
        {
            // If the bookmark title matches one of the specified titles, set its Open state
            foreach (string title in titles)
            {
                if (string.Equals(bm.Title, title, StringComparison.OrdinalIgnoreCase))
                {
                    bm.Open = openState; // false collapses the bookmark
                    break;
                }
            }

            // Process child bookmarks recursively, if any
            if (bm.ChildItems != null && bm.ChildItems.Count > 0)
            {
                SetBookmarksOpenState(bm.ChildItems, titles, openState);
            }
        }
    }
}