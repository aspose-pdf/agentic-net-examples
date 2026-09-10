using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_collapsed.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Titles of bookmarks that should be collapsed (closed) when the PDF is opened
        var titlesToCollapse = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Chapter 1",
            "Section 2"
        };

        // Use PdfBookmarkEditor to work with bookmarks
        var editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPdf);

        // Extract all existing bookmarks
        Bookmarks allBookmarks = editor.ExtractBookmarks();

        // Recursively set Open = false for the specified titles
        foreach (Bookmark bm in allBookmarks)
        {
            SetBookmarkOpenState(bm, titlesToCollapse);
        }

        // Remove existing bookmarks from the document
        editor.DeleteBookmarks();

        // Re‑create the (modified) bookmark hierarchy
        foreach (Bookmark bm in allBookmarks)
        {
            editor.CreateBookmarks(bm);
        }

        // Save the updated PDF
        editor.Save(outputPdf);
        editor.Close();

        Console.WriteLine($"Bookmarks updated and saved to '{outputPdf}'.");
    }

    // Recursively set the Open property based on the title list
    private static void SetBookmarkOpenState(Bookmark bookmark, HashSet<string> titlesToCollapse)
    {
        if (bookmark == null) return;

        if (titlesToCollapse.Contains(bookmark.Title))
        {
            // Collapse this bookmark
            bookmark.Open = false;
        }

        // Process child bookmarks, if any
        if (bookmark.ChildItems != null)
        {
            foreach (Bookmark child in bookmark.ChildItems)
            {
                SetBookmarkOpenState(child, titlesToCollapse);
            }
        }
    }
}