using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // PdfBookmarkEditor, Bookmark, Bookmarks

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";               // source PDF
        const string outputPdf = "output_bookmarked.pdf";   // PDF with hierarchical bookmarks

        // Ensure the source PDF exists – create a simple placeholder if it does not.
        if (!File.Exists(inputPdf))
        {
            var placeholder = new Document();
            // Add a few blank pages so the bookmark page numbers are valid.
            placeholder.Pages.Add(); // page 1
            placeholder.Pages.Add(); // page 2
            placeholder.Pages.Add(); // page 3
            placeholder.Pages.Add(); // page 4
            placeholder.Save(inputPdf);
        }

        // PdfBookmarkEditor implements IDisposable – use a using block for deterministic cleanup
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Bind the existing PDF file to the editor
            editor.BindPdf(inputPdf);

            // ---------- Build a hierarchy of bookmarks ----------
            // Child bookmarks for the first chapter
            Bookmarks chapter1Children = new Bookmarks();

            Bookmark section11 = new Bookmark
            {
                Title      = "Section 1.1",
                PageNumber = 2,          // points to page 2
                Action     = "GoTo"
            };
            Bookmark section12 = new Bookmark
            {
                Title      = "Section 1.2",
                PageNumber = 3,          // points to page 3
                Action     = "GoTo"
            };
            chapter1Children.Add(section11);
            chapter1Children.Add(section12);

            // Parent bookmark (Chapter 1) that contains the two sections above
            Bookmark chapter1 = new Bookmark
            {
                Title      = "Chapter 1",
                PageNumber = 1,          // points to page 1
                Action     = "GoTo",
                // Use the non‑obsolete property "ChildItems" instead of the deprecated "ChildItem"
                ChildItems = chapter1Children
            };

            // A second top‑level bookmark (Chapter 2) without children
            Bookmark chapter2 = new Bookmark
            {
                Title      = "Chapter 2",
                PageNumber = 4,          // points to page 4
                Action     = "GoTo"
            };

            // Add the bookmarks to the document.
            // Each call to CreateBookmarks adds the supplied bookmark to the root level.
            editor.CreateBookmarks(chapter1);
            editor.CreateBookmarks(chapter2);

            // Save the modified PDF with the new bookmark hierarchy
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Hierarchical bookmarks added. Output saved to '{outputPdf}'.");
    }
}
