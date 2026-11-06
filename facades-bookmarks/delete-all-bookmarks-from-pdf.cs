using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_bookmarks.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfBookmarkEditor handles bookmark operations.
        // It implements IDisposable, so wrap it in a using block.
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            // Load the PDF document.
            bookmarkEditor.BindPdf(inputPath);

            // Delete all bookmarks from the document.
            bookmarkEditor.DeleteBookmarks();

            // Save the modified PDF without bookmarks.
            bookmarkEditor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks removed. Saved to '{outputPath}'.");
    }
}