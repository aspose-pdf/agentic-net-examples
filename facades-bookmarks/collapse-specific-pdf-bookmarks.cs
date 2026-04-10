using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_collapsed.pdf";

        // Titles of bookmarks that should be collapsed (closed)
        string[] titlesToCollapse = { "Chapter 1", "Appendix A" };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the bookmark editor and bind the source PDF
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPdf);

        // Extract all bookmarks from the document
        Bookmarks allBookmarks = editor.ExtractBookmarks();

        // Iterate through the bookmark hierarchy and collapse matching titles
        foreach (Bookmark bm in allBookmarks)
        {
            CollapseMatchingBookmarks(bm, titlesToCollapse);
        }

        // Save the modified PDF
        editor.Save(outputPdf);
        Console.WriteLine($"Bookmarks collapsed and saved to '{outputPdf}'.");
    }

    // Recursively walks the bookmark tree and sets Open = false for matching titles
    static void CollapseMatchingBookmarks(Bookmark bookmark, string[] titlesToCollapse)
    {
        if (Array.Exists(titlesToCollapse, t => string.Equals(t, bookmark.Title, StringComparison.OrdinalIgnoreCase)))
        {
            bookmark.Open = false; // collapsed state
        }

        // Process child bookmarks if any
        if (bookmark.ChildItems != null)
        {
            foreach (Bookmark child in bookmark.ChildItems)
            {
                CollapseMatchingBookmarks(child, titlesToCollapse);
            }
        }
    }
}