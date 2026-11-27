using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_bookmarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfBookmarkEditor implements IDisposable, so wrap it in a using block.
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            // Bind the source PDF.
            bookmarkEditor.BindPdf(inputPath);

            // Create default bookmarks for all pages (one bookmark per page).
            bookmarkEditor.CreateBookmarks();

            // Add an additional bookmark titled "Additional Info" that points to page 5.
            // The facade provides CreateBookmarkOfPage for this purpose.
            bookmarkEditor.CreateBookmarkOfPage("Additional Info", 5);

            // Save the modified PDF with the new bookmarks.
            bookmarkEditor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
