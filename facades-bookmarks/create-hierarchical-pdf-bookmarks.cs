using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_bookmarked.pdf";

        // ---------------------------------------------------------------------
        // Create a minimal PDF so the example can run in an empty sandbox.
        // ---------------------------------------------------------------------
        using (Document seed = new Document())
        {
            // Add a few blank pages – enough for the bookmark page numbers.
            seed.Pages.Add(); // page 1
            seed.Pages.Add(); // page 2
            seed.Pages.Add(); // page 3
            seed.Pages.Add(); // page 4
            seed.Save(inputPdf);
        }

        // Bind the PDF to the bookmark editor and create hierarchical bookmarks
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Initialize the facade with the source PDF
            editor.BindPdf(inputPdf);

            // ----- Chapter 1 -----
            Bookmark chapter1 = new Bookmark
            {
                Title      = "Chapter 1",
                PageNumber = 1,
                Action     = "GoTo"
            };

            // Sections under Chapter 1
            Bookmark sec11 = new Bookmark
            {
                Title      = "Section 1.1",
                PageNumber = 2,
                Action     = "GoTo"
            };
            Bookmark sec12 = new Bookmark
            {
                Title      = "Section 1.2",
                PageNumber = 3,
                Action     = "GoTo"
            };

            // Attach sections to Chapter 1 using the non‑obsolete ChildItems property
            Bookmarks chapter1Children = new Bookmarks();
            chapter1Children.Add(sec11);
            chapter1Children.Add(sec12);
            chapter1.ChildItems = chapter1Children; // <-- fixed property

            // ----- Chapter 2 -----
            Bookmark chapter2 = new Bookmark
            {
                Title      = "Chapter 2",
                PageNumber = 4,
                Action     = "GoTo"
            };

            // Add top‑level chapters to the document
            editor.CreateBookmarks(chapter1);
            editor.CreateBookmarks(chapter2);

            // Save the modified PDF with the new bookmark hierarchy
            editor.Save(outputPdf);
            // editor.Close() is optional because the using‑statement disposes it
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPdf}'.");
    }
}
