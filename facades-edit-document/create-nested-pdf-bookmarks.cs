using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string outputPdf = "output_with_bookmarks.pdf";

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the bookmark editor and bind the PDF
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPdf);

        // ----- Build a three‑level bookmark hierarchy -----
        // Level 3 (sub‑subsection)
        Bookmark subSub = new Bookmark
        {
            Title      = "Sub‑subsection 1.1.1",
            PageNumber = 3
        };

        // Level 2 (subsection) – attach the level‑3 bookmark as a child
        Bookmark sub = new Bookmark
        {
            Title      = "Subsection 1.1",
            PageNumber = 2,
            ChildItems = new Bookmarks()
        };
        sub.ChildItems.Add(subSub);

        // Level 1 (section) – attach the level‑2 bookmark as a child
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