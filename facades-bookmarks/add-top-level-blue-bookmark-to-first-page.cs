using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Use PdfBookmarkEditor (facade) to manipulate bookmarks.
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Load the PDF document.
            editor.BindPdf(inputPath);

            // Create a top‑level bookmark that points to page 1.
            Bookmark bookmark = new Bookmark
            {
                Title      = "First Page",
                PageNumber = 1,
                // Set the bookmark title color to blue (System.Drawing.Color).
                TitleColor = System.Drawing.Color.Blue
            };

            // Add the bookmark to the document.
            editor.CreateBookmarks(bookmark);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}
