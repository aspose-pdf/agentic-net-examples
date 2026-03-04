using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

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

        // Initialize the bookmark editor facade
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        try
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPdf);

            // ----- Build nested bookmark hierarchy -----
            // Parent bookmark (e.g., a chapter)
            Bookmark parent = new Bookmark
            {
                Title      = "Chapter 1",
                PageNumber = 1,
                Action     = "GoTo"
            };

            // Child bookmarks (e.g., sections)
            Bookmark child1 = new Bookmark
            {
                Title      = "Section 1.1",
                PageNumber = 2,
                Action     = "GoTo"
            };

            Bookmark child2 = new Bookmark
            {
                Title      = "Section 1.2",
                PageNumber = 3,
                Action     = "GoTo"
            };

            // Assemble children into a collection and attach to parent
            Bookmarks children = new Bookmarks();
            children.Add(child1);
            children.Add(child2);
            parent.ChildItem = children;

            // Create the nested bookmarks in the PDF
            editor.CreateBookmarks(parent);

            // Save the modified PDF
            editor.Save(outputPdf);
        }
        finally
        {
            // Ensure resources are released
            editor.Close();
        }

        Console.WriteLine($"Nested bookmarks added and saved to '{outputPdf}'.");
    }
}