using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string bookmarkTitle = "Editor Reference";
        const int bookmarkPage = 13; // 1‑based page number

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfBookmarkEditor (the Facades class that supports bookmark operations).
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Bind the existing PDF.
            editor.BindPdf(inputPath);

            // Create a bookmark that points to the specified page.
            // The method name is CreateBookmarkOfPage (not AddBookmark).
            editor.CreateBookmarkOfPage(bookmarkTitle, bookmarkPage);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}
