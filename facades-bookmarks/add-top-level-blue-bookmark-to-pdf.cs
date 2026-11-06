using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_bookmarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Bind the existing PDF to the bookmark editor
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            bookmarkEditor.BindPdf(inputPath);

            // Create a bookmark that points to page 1 and set its color to blue
            Bookmark bookmark = new Bookmark
            {
                Title = "First Page Bookmark",
                PageNumber = 1,
                Action = "GoTo",               // required for a page‑goto bookmark
                TitleColor = System.Drawing.Color.Blue // fully qualified to avoid ambiguity
            };

            // Add the bookmark to the document (top‑level)
            bookmarkEditor.CreateBookmarks(bookmark);

            // Save the modified PDF
            bookmarkEditor.Save(outputPath);
        }

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}
