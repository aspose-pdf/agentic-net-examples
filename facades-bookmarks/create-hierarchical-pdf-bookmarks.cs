using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_bookmarks.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document (required for proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the bookmark editor and bind the loaded document
            Aspose.Pdf.Facades.PdfBookmarkEditor bookmarkEditor = new Aspose.Pdf.Facades.PdfBookmarkEditor();
            bookmarkEditor.BindPdf(doc);

            // ------------------------------------------------------------
            // Build a hierarchical bookmark structure.
            // Example structure:
            //   Chapter 1 (page 1)
            //     Section 1.1 (page 2)
            //     Section 1.2 (page 3)
            //   Chapter 2 (page 5)
            // ------------------------------------------------------------

            // Child bookmarks for Chapter 1
            Aspose.Pdf.Facades.Bookmark section1 = new Aspose.Pdf.Facades.Bookmark
            {
                Title      = "Section 1.1",
                PageNumber = 2,
                Action     = "GoTo"
            };

            Aspose.Pdf.Facades.Bookmark section2 = new Aspose.Pdf.Facades.Bookmark
            {
                Title      = "Section 1.2",
                PageNumber = 3,
                Action     = "GoTo"
            };

            // Container for Chapter 1 children
            Aspose.Pdf.Facades.Bookmarks chapter1Children = new Aspose.Pdf.Facades.Bookmarks();
            chapter1Children.Add(section1);
            chapter1Children.Add(section2);

            // Chapter 1 bookmark (parent)
            Aspose.Pdf.Facades.Bookmark chapter1 = new Aspose.Pdf.Facades.Bookmark
            {
                Title      = "Chapter 1",
                PageNumber = 1,
                Action     = "GoTo",
                ChildItems = chapter1Children
            };

            // Chapter 2 bookmark (no children in this example)
            Aspose.Pdf.Facades.Bookmark chapter2 = new Aspose.Pdf.Facades.Bookmark
            {
                Title      = "Chapter 2",
                PageNumber = 5,
                Action     = "GoTo"
            };

            // Top‑level collection of bookmarks
            Aspose.Pdf.Facades.Bookmarks topLevel = new Aspose.Pdf.Facades.Bookmarks();
            topLevel.Add(chapter1);
            topLevel.Add(chapter2);

            // Add each top‑level bookmark to the document.
            // The CreateBookmarks(Bookmark) method can be called for each root bookmark.
            foreach (Aspose.Pdf.Facades.Bookmark rootBookmark in topLevel)
            {
                bookmarkEditor.CreateBookmarks(rootBookmark);
            }

            // Save the modified PDF with the new bookmark hierarchy
            bookmarkEditor.Save(outputPdf);

            // Clean up the facade (optional, as it will be disposed when the program ends)
            bookmarkEditor.Close();
        }

        Console.WriteLine($"PDF with hierarchical bookmarks saved to '{outputPdf}'.");
    }
}