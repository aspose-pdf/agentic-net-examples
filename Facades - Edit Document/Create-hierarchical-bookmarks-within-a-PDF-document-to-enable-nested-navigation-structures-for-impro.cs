using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfBookmarkEditor, Bookmark, Bookmarks

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

        // Create the facade instance (creation rule)
        PdfBookmarkEditor editor = new PdfBookmarkEditor();

        // Bind the existing PDF (load rule)
        editor.BindPdf(inputPdf);

        // Build hierarchical bookmarks
        // Parent bookmark
        Bookmark parent = new Bookmark
        {
            Title      = "Chapter 1",
            PageNumber = 1,
            Open       = true               // expanded in the bookmark pane
        };

        // First child bookmark
        Bookmark child1 = new Bookmark
        {
            Title      = "Section 1.1",
            PageNumber = 2
        };

        // Second child bookmark
        Bookmark child2 = new Bookmark
        {
            Title      = "Section 1.2",
            PageNumber = 3
        };

        // Attach children to the parent
        Bookmarks children = new Bookmarks();
        children.Add(child1);
        children.Add(child2);
        parent.ChildItems = children;

        // Create the hierarchical bookmark structure in the PDF (creation rule)
        editor.CreateBookmarks(parent);

        // Save the modified PDF (save rule)
        editor.Save(outputPdf);

        // Release resources
        editor.Close();

        Console.WriteLine($"Hierarchical bookmarks added and saved to '{outputPdf}'.");
    }
}