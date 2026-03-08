using System;
using Aspose.Pdf;               // Document, Page
using Aspose.Pdf.Facades;      // PdfBookmarkEditor, Bookmark, Bookmarks

class Program
{
    static void Main()
    {
        // Paths for the output PDF (the input PDF is created in‑memory)
        const string outputPath = "output_bookmarks.pdf";

        // ------------------------------------------------------------
        // Create a minimal PDF document in memory (one blank page).
        // This avoids a FileNotFoundException when the source file does
        // not exist on disk.
        // ------------------------------------------------------------
        Document doc = new Document();
        doc.Pages.Add(); // ensure at least one page so page numbers are valid

        // Initialise the bookmark editor and bind the in‑memory document
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(doc);

        // ------------------------------------------------------------
        // Build a hierarchical bookmark structure:
        //   Chapter 1 (page 1)
        //     ├─ Section 1.1 (page 2)
        //     └─ Section 1.2 (page 3)
        //           └─ Subsection 1.2.1 (page 4)
        // ------------------------------------------------------------

        // Top‑level bookmark (parent)
        Bookmark parent = new Bookmark
        {
            Title      = "Chapter 1",
            PageNumber = 1,
            Action     = "GoTo"
        };

        // First child bookmark
        Bookmark child1 = new Bookmark
        {
            Title      = "Section 1.1",
            PageNumber = 2,
            Action     = "GoTo"
        };

        // Second child bookmark (will have its own child)
        Bookmark child2 = new Bookmark
        {
            Title      = "Section 1.2",
            PageNumber = 3,
            Action     = "GoTo"
        };

        // Sub‑child of the second child
        Bookmark subChild = new Bookmark
        {
            Title      = "Subsection 1.2.1",
            PageNumber = 4,
            Action     = "GoTo"
        };

        // Attach sub‑child to child2 using the (non‑obsolete) ChildItems property
        child2.ChildItems = new Bookmarks();
        child2.ChildItems.Add(subChild);

        // Attach child1 and child2 to the parent bookmark using ChildItems
        parent.ChildItems = new Bookmarks();
        parent.ChildItems.Add(child1);
        parent.ChildItems.Add(child2);

        // Add the constructed hierarchy to the PDF document
        editor.CreateBookmarks(parent);

        // Save the modified PDF containing the nested bookmarks
        editor.Save(outputPath);
    }
}
