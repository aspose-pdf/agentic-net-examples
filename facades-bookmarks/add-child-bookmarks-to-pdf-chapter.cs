using System;
using System.IO;
using System.Drawing;               // For Color
using Aspose.Pdf.Facades;          // PdfBookmarkEditor, Bookmark, Bookmarks

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the bookmark editor and bind the source PDF
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPdf);

        // Create the parent bookmark (e.g., a chapter)
        Bookmark parent = new Bookmark
        {
            Title      = "Chapter 1",
            Action     = "GoTo",   // Navigate to a page
            PageNumber = 1,
            Open       = true      // Expanded in the bookmark pane
        };

        // Create child bookmarks representing subsections
        Bookmark child1 = new Bookmark
        {
            Title      = "Section 1.1",
            Action     = "GoTo",
            PageNumber = 2,
            Open       = false
        };

        Bookmark child2 = new Bookmark
        {
            Title      = "Section 1.2",
            Action     = "GoTo",
            PageNumber = 3,
            Open       = false
        };

        // Assemble the child collection
        Bookmarks children = new Bookmarks();
        children.Add(child1);
        children.Add(child2);
        parent.ChildItems = children;

        // Add the parent (with its children) to the PDF
        editor.CreateBookmarks(parent);

        // Save the modified PDF
        editor.Save(outputPdf);
        editor.Close();

        Console.WriteLine($"Bookmarks added and saved to '{outputPdf}'.");
    }
}