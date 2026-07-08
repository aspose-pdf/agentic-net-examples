using System;
using System.Drawing;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Facades; // PdfBookmarkEditor resides here

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfBookmarkEditor to add a top‑level bookmark linking to page 1
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPath);

            // Create a bookmark object, set its title, destination page and color
            Bookmark bm = new Bookmark
            {
                Title       = "First Page Bookmark",
                PageNumber  = 1,                 // link to the first page (1‑based)
                TitleColor  = Color.Blue         // blue color for the bookmark title
            };

            // Add the bookmark to the document
            editor.CreateBookmarks(bm);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}