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

        // PdfBookmarkEditor implements IDisposable, so wrap it in a using block.
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            // Bind the source PDF file.
            bookmarkEditor.BindPdf(inputPath);

            // Create default bookmarks – one for each page.
            bookmarkEditor.CreateBookmarks();

            // Add an extra bookmark titled "Extra" that points to page 3.
            // Use the dedicated method for a single page bookmark.
            bookmarkEditor.CreateBookmarkOfPage("Extra", 3);

            // Save the modified PDF with the new bookmarks.
            bookmarkEditor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks updated and saved to '{outputPath}'.");
    }
}
