using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "output_with_bookmarks.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // PdfBookmarkEditor implements IDisposable, so use a using block.
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Load the PDF document into the editor.
            editor.BindPdf(inputPdf);

            // ----- Build hierarchical bookmarks -----
            // Parent bookmark (e.g., Chapter 1)
            Bookmark parent = new Bookmark
            {
                Title      = "Chapter 1",
                PageNumber = 1
            };

            // First child bookmark (e.g., Section 1.1)
            Bookmark child1 = new Bookmark
            {
                Title      = "Section 1.1",
                PageNumber = 2
            };

            // Second child bookmark (e.g., Section 1.2)
            Bookmark child2 = new Bookmark
            {
                Title      = "Section 1.2",
                PageNumber = 3
            };

            // Assemble children into a collection and attach to the parent.
            Bookmarks children = new Bookmarks();
            children.Add(child1);
            children.Add(child2);
            parent.ChildItem = children;   // hierarchical link

            // Create the bookmark hierarchy in the PDF.
            editor.CreateBookmarks(parent);

            // Save the modified PDF.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Hierarchical bookmarks added. Output saved to '{outputPdf}'.");
    }
}