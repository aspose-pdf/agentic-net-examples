using System;
using System.Drawing;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Verify that the source PDF exists.
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // ------------------------------------------------------------
        // Build a nested bookmark hierarchy:
        //   Parent (Chapter 1)
        //      ├─ Child 1 (Section 1.1)
        //      └─ Child 2 (Section 1.2)
        // ------------------------------------------------------------

        // Parent bookmark (will contain the children).
        Bookmark parent = new Bookmark
        {
            Title = "Chapter 1",
            PageNumber = 1,   // Destination page for the parent.
            Open = true       // Expanded when the PDF is opened.
        };

        // First child bookmark.
        Bookmark child1 = new Bookmark
        {
            Title = "Section 1.1",
            PageNumber = 2
        };

        // Second child bookmark.
        Bookmark child2 = new Bookmark
        {
            Title = "Section 1.2",
            PageNumber = 3
        };

        // Assemble the child collection.
        Bookmarks childCollection = new Bookmarks();
        childCollection.Add(child1);
        childCollection.Add(child2);

        // Attach the children to the parent.
        parent.ChildItems = childCollection;

        // ------------------------------------------------------------
        // Apply the bookmarks to the PDF using PdfBookmarkEditor.
        // ------------------------------------------------------------
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Bind the existing PDF file.
            editor.BindPdf(inputPdf);

            // Insert the nested bookmark hierarchy.
            editor.CreateBookmarks(parent);

            // (Optional) Create simple page bookmarks with a specific color.
            // editor.CreateBookmarks(Color.Blue, boldFlag: true, italicFlag: false);

            // Save the modified PDF.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Nested bookmarks have been added and saved to '{outputPdf}'.");
    }
}