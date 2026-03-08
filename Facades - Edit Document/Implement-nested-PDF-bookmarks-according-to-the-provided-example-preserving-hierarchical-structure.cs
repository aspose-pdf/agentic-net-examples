using System;
using System.IO;
using System.Drawing;               // for Color
using Aspose.Pdf.Facades;          // PdfBookmarkEditor, Bookmark, Bookmarks

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_bookmarks.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the bookmark editor and bind it to the source PDF
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPdf);

        // ------------------------------------------------------------
        // Build a nested bookmark hierarchy:
        //   Parent (Chapter 1) -> Child 1 (Section 1.1)
        //                     -> Child 2 (Section 1.2)
        // ------------------------------------------------------------

        // Parent bookmark (level 1)
        Bookmark parent = new Bookmark
        {
            Title      = "Chapter 1",
            PageNumber = 1,          // Destination page for the parent
            BoldFlag   = true,
            ItalicFlag = false,
            TitleColor = Color.DarkBlue
        };

        // Child bookmark 1 (level 2)
        Bookmark child1 = new Bookmark
        {
            Title      = "Section 1.1",
            PageNumber = 2,
            BoldFlag   = false,
            ItalicFlag = false,
            TitleColor = Color.Black
        };

        // Child bookmark 2 (level 2)
        Bookmark child2 = new Bookmark
        {
            Title      = "Section 1.2",
            PageNumber = 3,
            BoldFlag   = false,
            ItalicFlag = false,
            TitleColor = Color.Black
        };

        // Attach children to the parent using a Bookmarks collection
        Bookmarks childCollection = new Bookmarks();
        childCollection.Add(child1);
        childCollection.Add(child2);
        parent.ChildItems = childCollection;

        // Create the nested bookmarks in the PDF
        editor.CreateBookmarks(parent);

        // Save the modified PDF with the new bookmark structure
        editor.Save(outputPdf);

        // Clean up the editor (optional, as it does not implement IDisposable)
        editor.Close();

        Console.WriteLine($"Nested bookmarks added and saved to '{outputPdf}'.");
    }
}