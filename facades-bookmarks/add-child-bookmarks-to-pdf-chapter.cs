using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfBookmarkEditor, Bookmark, Bookmarks

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string parentTitle = "Chapter 1";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the bookmark editor and bind the PDF file
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPdf);

        // Remove any existing bookmark with the same parent title to avoid duplicates
        editor.DeleteBookmarks(parentTitle);

        // Create the parent bookmark (e.g., a chapter)
        Bookmark parent = new Bookmark
        {
            Title      = parentTitle,
            PageNumber = 1,   // page where the chapter starts
            Open       = true // expanded view in the bookmark pane
        };

        // Create child bookmarks representing subsections within the chapter
        Bookmark child1 = new Bookmark { Title = "Section 1.1", PageNumber = 2 };
        Bookmark child2 = new Bookmark { Title = "Section 1.2", PageNumber = 3 };
        Bookmark child3 = new Bookmark { Title = "Section 1.3", PageNumber = 4 };

        // Assemble the child bookmarks into a collection
        Bookmarks children = new Bookmarks();
        children.Add(child1);
        children.Add(child2);
        children.Add(child3);

        // Attach the child collection to the parent bookmark
        parent.ChildItems = children;

        // Add the hierarchical bookmark (parent with its children) to the PDF
        editor.CreateBookmarks(parent);

        // Save the modified PDF to a new file
        editor.Save(outputPdf);

        // Release resources held by the editor
        editor.Close();
    }
}