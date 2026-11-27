using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "bookmarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfBookmarkEditor does not implement IDisposable, so we instantiate it directly.
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

        // Bind the source PDF document.
        bookmarkEditor.BindPdf(inputPath);

        // Create a bookmark titled "Summary" that points to page 15.
        bookmarkEditor.CreateBookmarkOfPage("Summary", 15);

        // Create a bookmark titled "Appendix" that points to page 20.
        bookmarkEditor.CreateBookmarkOfPage("Appendix", 20);

        // Save the PDF with the new bookmarks.
        bookmarkEditor.Save(outputPath);

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
