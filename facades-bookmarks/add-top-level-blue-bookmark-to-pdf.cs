using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing; // System.Drawing.Color is required for Bookmark.TitleColor

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a top‑level bookmark that points to the first page
        Bookmark bookmark = new Bookmark
        {
            Title = "First Page Bookmark", // bookmark title
            PageNumber = 1,                // 1‑based page index
            TitleColor = Color.Blue        // set the title color to blue
        };

        // Use PdfBookmarkEditor (facade) to add the bookmark and save the PDF
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPath);          // load the PDF
            editor.CreateBookmarks(bookmark);   // add the prepared bookmark
            editor.Save(outputPath);            // write the updated PDF
        }

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}