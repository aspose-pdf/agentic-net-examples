using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfBookmarkEditor implements IDisposable, so use a using block.
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Bind the existing PDF file.
            editor.BindPdf(inputPath);

            // Create the parent bookmark "Chapter Z".
            Bookmark parent = new Bookmark
            {
                Title      = "Chapter Z",
                PageNumber = 1,          // target page number (1‑based)
                Action     = "GoTo"    // action is a plain string in the facade model
            };

            // Create the child bookmark "Section Z1".
            Bookmark child = new Bookmark
            {
                Title      = "Section Z1",
                PageNumber = 1,
                Action     = "GoTo"
            };

            // Attach the child to the parent using the ChildItems collection.
            parent.ChildItems.Add(child);

            // Add the bookmark hierarchy to the PDF. CreateBookmarks accepts a single Bookmark.
            editor.CreateBookmarks(parent);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
