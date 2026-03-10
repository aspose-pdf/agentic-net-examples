using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfBookmarkEditor, Bookmark, Bookmarks
using Aspose.Pdf;                 // Document (if needed), Color

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string parentTitle = "Parent Bookmark";   // title of the existing parent
        const string newChildTitle = "New Child Bookmark";
        const int    newChildPage  = 3;                 // page number for the new child

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the bookmark editor and bind the source PDF
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPdf);

            // Extract all existing bookmarks
            Bookmarks allBookmarks = editor.ExtractBookmarks();

            // Locate the parent bookmark (by title)
            Bookmark parentBookmark = null;
            foreach (Bookmark bm in allBookmarks)
            {
                if (string.Equals(bm.Title, parentTitle, StringComparison.OrdinalIgnoreCase))
                {
                    parentBookmark = bm;
                    break;
                }
            }

            if (parentBookmark == null)
            {
                Console.Error.WriteLine($"Parent bookmark \"{parentTitle}\" not found.");
                return;
            }

            // Ensure the parent has a child collection
            if (parentBookmark.ChildItems == null)
                parentBookmark.ChildItems = new Bookmarks();

            // Create the new child bookmark
            Bookmark newChild = new Bookmark
            {
                Title      = newChildTitle,
                PageNumber = newChildPage,
                Open       = true
            };

            // Add the new child to the parent's child collection
            parentBookmark.ChildItems.Add(newChild);

            // Remove all existing bookmarks to avoid duplication
            editor.DeleteBookmarks();

            // Re‑create the modified hierarchy (parent with its children)
            editor.CreateBookmarks(parentBookmark);

            // Save the updated PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Bookmark hierarchy updated and saved to '{outputPdf}'.");
    }
}