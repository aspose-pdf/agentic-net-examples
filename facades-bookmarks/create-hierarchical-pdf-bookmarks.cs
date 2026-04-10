using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // PdfBookmarkEditor, Bookmark, Bookmarks

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_bookmarked.pdf";

        // Ensure a source PDF exists – create a simple one if missing
        if (!File.Exists(inputPdf))
        {
            using (Document doc = new Document())
            {
                // Add a single blank page
                doc.Pages.Add();
                doc.Save(inputPdf);
            }
        }

        // Example chapter titles and their corresponding page numbers
        string[] chapterTitles = { "Chapter 1", "Chapter 2", "Chapter 3" };
        int[]    chapterPages  = { 1, 5, 9 };

        // Initialize the bookmark editor and bind the source PDF
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPdf);

        // Build a root bookmark (optional – can be omitted if not needed)
        Bookmark rootBookmark = new Bookmark
        {
            Title      = "Document Outline",
            Action     = "GoTo",
            PageNumber = 1 // root can point to the first page
        };

        // Create a collection for child bookmarks (one per chapter)
        Bookmarks chapterBookmarks = new Bookmarks();

        for (int i = 0; i < chapterTitles.Length; i++)
        {
            Bookmark chapter = new Bookmark
            {
                Title      = chapterTitles[i],
                PageNumber = chapterPages[i],
                Action     = "GoTo"
            };
            chapterBookmarks.Add(chapter);
        }

        // Attach the child collection to the root bookmark using the non‑obsolete property
        rootBookmark.ChildItems = chapterBookmarks;

        // Add the hierarchical bookmark structure to the PDF
        editor.CreateBookmarks(rootBookmark);

        // Save the modified PDF with the new bookmarks
        editor.Save(outputPdf);
    }
}
