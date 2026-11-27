using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_bookmarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfBookmarkEditor does not implement IDisposable, manage its lifetime manually.
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

        // Bind the existing PDF document.
        bookmarkEditor.BindPdf(inputPath);

        // Create a bookmark titled "Cover" that points to the first page.
        // The correct method is CreateBookmarkOfPage (returns void) – it adds the bookmark directly.
        bookmarkEditor.CreateBookmarkOfPage("Cover", 1);

        // Save the modified PDF.
        bookmarkEditor.Save(outputPath);
        bookmarkEditor.Close();

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}
