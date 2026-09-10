using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfBookmarkEditor, Bookmark, Bookmarks

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_subbookmarks.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the bookmark editor and bind the source PDF
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPdf);

        // ----- Create parent bookmark (e.g., a chapter) -----
        Bookmark parentBookmark = new Bookmark
        {
            Title      = "Chapter 1: Introduction",
            PageNumber = 1,               // Destination page for the parent
            Open       = true             // Expanded by default
        };

        // ----- Create child bookmarks (subsections) -----
        Bookmark child1 = new Bookmark
        {
            Title      = "Section 1.1: Background",
            PageNumber = 2,
            Open       = false
        };

        Bookmark child2 = new Bookmark
        {
            Title      = "Section 1.2: Objectives",
            PageNumber = 3,
            Open       = false
        };

        // Assemble child bookmarks into a collection
        Bookmarks childCollection = new Bookmarks();
        childCollection.Add(child1);
        childCollection.Add(child2);

        // Attach the child collection to the parent
        parentBookmark.ChildItems = childCollection;

        // Add the hierarchical bookmark structure to the PDF
        editor.CreateBookmarks(parentBookmark);

        // Save the modified PDF
        editor.Save(outputPdf);

        // Release resources held by the editor
        editor.Close();

        Console.WriteLine($"PDF saved with hierarchical bookmarks: {outputPdf}");
    }
}