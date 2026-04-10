using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    // Recursively search for a bookmark with the specified title.
    static Bookmark FindBookmarkByTitle(Bookmark root, string title)
    {
        if (root == null) return null;
        if (root.Title == title) return root;

        // Use the non‑obsolete ChildItems collection.
        if (root.ChildItems != null)
        {
            foreach (Bookmark child in root.ChildItems)
            {
                Bookmark found = FindBookmarkByTitle(child, title);
                if (found != null) return found;
            }
        }
        return null;
    }

    // Search the top‑level collection for the bookmark.
    static Bookmark FindBookmarkInCollection(Bookmarks collection, string title)
    {
        if (collection == null) return null;
        foreach (Bookmark bm in collection)
        {
            var result = FindBookmarkByTitle(bm, title);
            if (result != null) return result;
        }
        return null;
    }

    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Identifier of the bookmark to modify (using its title here).
        const string bookmarkTitle = "TargetBookmark";

        // Desired destination page (Aspose.Pdf uses 1‑based indexing).
        const int destinationPage = 10;

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the PDF to the bookmark editor, modify the bookmark, and save.
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Load the PDF document.
            editor.BindPdf(inputPdf);

            // Extract the root bookmark hierarchy (returns a Bookmarks collection).
            Bookmarks rootBookmarks = editor.ExtractBookmarks();

            // Locate the bookmark with the given title.
            Bookmark target = FindBookmarkInCollection(rootBookmarks, bookmarkTitle);
            if (target != null)
            {
                // Set the destination to page 10 (as a string).
                target.Destination = destinationPage.ToString();
            }
            else
            {
                Console.Error.WriteLine($"Bookmark titled '{bookmarkTitle}' not found.");
            }

            // Save the updated PDF.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Updated bookmark saved to '{outputPdf}'.");
    }
}
