using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfBookmarkEditor, Bookmark, Bookmarks
using Aspose.Pdf;           // Document (if needed for other operations)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF
        const string outputPath = "output_bookmarked.pdf"; // destination PDF
        const int startPage = 2;   // first page of the range (1‑based)
        const int endPage   = 5;   // last page of the range (inclusive)

        // Validate input file and page range
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (startPage < 1 || endPage < startPage)
        {
            Console.Error.WriteLine("Invalid page range specified.");
            return;
        }

        // PdfBookmarkEditor implements SaveableFacade (IDisposable), so use using
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Load the PDF into the editor
            editor.BindPdf(inputPath);

            // Create a parent bookmark that represents the whole range
            Bookmark parent = new Bookmark
            {
                Title      = $"Pages {startPage}-{endPage}",
                PageNumber = startPage   // navigation points to the first page of the range
            };

            // Prepare a collection for child bookmarks (one per page)
            Bookmarks childBookmarks = new Bookmarks();

            // Generate a child bookmark for each page in the specified range
            for (int page = startPage; page <= endPage; page++)
            {
                Bookmark child = new Bookmark
                {
                    Title      = $"Page {page}",
                    PageNumber = page
                };
                childBookmarks.Add(child);
            }

            // Attach the child collection to the parent bookmark
            parent.ChildItem = childBookmarks;

            // Add the hierarchical bookmark structure to the PDF
            editor.CreateBookmarks(parent);

            // Persist the changes to a new file
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks for pages {startPage}-{endPage} added successfully to '{outputPath}'.");
    }
}