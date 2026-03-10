using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_bookmarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF into the bookmark editor
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPath); // Load rule

        // ----- Build hierarchical bookmarks -----
        // Parent bookmark (Chapter 1)
        Aspose.Pdf.Facades.Bookmark parent = new Aspose.Pdf.Facades.Bookmark
        {
            Title = "Chapter 1",
            PageNumber = 1,
            Action = "GoTo"
        };

        // Child bookmark 1 (Section 1.1)
        Aspose.Pdf.Facades.Bookmark child1 = new Aspose.Pdf.Facades.Bookmark
        {
            Title = "Section 1.1",
            PageNumber = 2,
            Action = "GoTo"
        };

        // Child bookmark 2 (Section 1.2)
        Aspose.Pdf.Facades.Bookmark child2 = new Aspose.Pdf.Facades.Bookmark
        {
            Title = "Section 1.2",
            PageNumber = 3,
            Action = "GoTo"
        };

        // Attach children to the parent
        Aspose.Pdf.Facades.Bookmarks childCollection = new Aspose.Pdf.Facades.Bookmarks();
        childCollection.Add(child1);
        childCollection.Add(child2);
        parent.ChildItem = childCollection;

        // Another top‑level bookmark (Chapter 2)
        Aspose.Pdf.Facades.Bookmark topLevel2 = new Aspose.Pdf.Facades.Bookmark
        {
            Title = "Chapter 2",
            PageNumber = 4,
            Action = "GoTo"
        };

        // ----- Add bookmarks to the document -----
        // CreateBookmarks(Bookmark) forms a nested hierarchy when the bookmark has children
        editor.CreateBookmarks(parent);      // adds Chapter 1 with its sections
        editor.CreateBookmarks(topLevel2);   // adds Chapter 2

        // Save the updated PDF (save rule)
        editor.Save(outputPath);
        editor.Close(); // release resources

        Console.WriteLine($"Nested bookmarks created and saved to '{outputPath}'.");
    }
}