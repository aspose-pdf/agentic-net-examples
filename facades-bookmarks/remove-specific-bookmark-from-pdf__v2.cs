using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string bookmarkTitle = "Appendix";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfBookmarkEditor does not implement IDisposable, so we instantiate it directly.
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

        // Bind the source PDF.
        bookmarkEditor.BindPdf(inputPath);

        // Delete the bookmark with the specified title. Use the one‑argument overload of DeleteBookmarks.
        bookmarkEditor.DeleteBookmarks(bookmarkTitle);

        // Save the modified PDF.
        bookmarkEditor.Save(outputPath);

        Console.WriteLine($"Bookmark '{bookmarkTitle}' removed. Saved to '{outputPath}'.");
    }
}
