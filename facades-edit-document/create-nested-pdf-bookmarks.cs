using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfBookmarkEditor, Bookmark, Bookmarks

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_bookmarks.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the bookmark editor and bind the source PDF
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPdf);

        // ----- Build the bookmark hierarchy (3 levels) -----
        // Sub‑subsection (level 3)
        Bookmark subSub = new Bookmark
        {
            Title      = "Sub‑subsection 1.1.1",
            PageNumber = 3
        };

        // Subsection (level 2) – add the sub‑subsection as a child
        Bookmark sub = new Bookmark
        {
            Title      = "Subsection 1.1",
            PageNumber = 2,
            ChildItems = new Bookmarks()
        };
        sub.ChildItems.Add(subSub);

        // Section (level 1) – add the subsection as a child
        Bookmark parent = new Bookmark
        {
            Title      = "Section 1",
            PageNumber = 1,
            ChildItems = new Bookmarks()
        };
        parent.ChildItems.Add(sub);

        // Create the nested bookmarks in the document
        editor.CreateBookmarks(parent);

        // Save the modified PDF
        editor.Save(outputPdf);

        Console.WriteLine($"Nested bookmarks created and saved to '{outputPdf}'.");
    }
}