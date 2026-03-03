using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // PdfBookmarkEditor implements IDisposable, so wrap it in a using block.
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Bind the existing PDF file.
            editor.BindPdf(inputPdf);

            // ----- Build a nested bookmark hierarchy -----
            // Parent bookmark (will appear at the top level).
            Bookmark parent = new Bookmark
            {
                Title      = "Parent Bookmark",
                PageNumber = 1,          // Destination page for the parent.
                Action     = "GoTo"      // Explicit action type.
            };

            // Child bookmark (nested under the parent).
            Bookmark child = new Bookmark
            {
                Title      = "Child Bookmark",
                PageNumber = 2,
                Action     = "GoTo"
            };

            // Attach the child to the parent via a Bookmarks collection.
            Bookmarks childItems = new Bookmarks();
            childItems.Add(child);
            parent.ChildItem = childItems;

            // Add the hierarchy to the PDF.
            editor.CreateBookmarks(parent);

            // Save the modified PDF to a new file.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Bookmark hierarchy added and saved to '{outputPdf}'.");
    }
}