using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Facades; // PdfBookmarkEditor and Bookmark classes
using Aspose.Pdf; // for Document if needed (not used directly here)

class Program
{
    // Recursively collapse bookmarks whose titles match the specified list
    static void CollapseBookmarks(Bookmark bookmark, string[] titlesToCollapse)
    {
        foreach (string title in titlesToCollapse)
        {
            if (string.Equals(bookmark.Title, title, StringComparison.OrdinalIgnoreCase))
            {
                bookmark.Open = false; // set to collapsed
                break;
            }
        }

        // Process child bookmarks recursively
        if (bookmark.ChildItems != null)
        {
            foreach (Bookmark child in bookmark.ChildItems)
            {
                CollapseBookmarks(child, titlesToCollapse);
            }
        }
    }

    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_collapsed.pdf";

        // Titles of bookmarks that should be collapsed initially
        string[] titlesToCollapse = { "Chapter 1", "Section 2" };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the bookmark editor and bind the PDF
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPdf);

        // Extract all existing bookmarks
        Bookmarks allBookmarks = editor.ExtractBookmarks();

        // Collapse the specified bookmarks
        foreach (Bookmark bm in allBookmarks)
        {
            CollapseBookmarks(bm, titlesToCollapse);
        }

        // Remove existing bookmarks from the document
        editor.DeleteBookmarks();

        // Re‑add the (now modified) bookmarks preserving hierarchy
        foreach (Bookmark bm in allBookmarks)
        {
            editor.CreateBookmarks(bm);
        }

        // Save the updated PDF
        editor.Save(outputPdf);

        // Clean up
        editor.Close();

        Console.WriteLine($"Bookmarks updated and saved to '{outputPdf}'.");
    }
}