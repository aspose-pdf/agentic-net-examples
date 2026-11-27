using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "bookmarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfBookmarkEditor does not implement IDisposable, a plain instance is sufficient.
        var editor = new PdfBookmarkEditor();

        // Load and bind the source PDF.
        editor.BindPdf(inputPath);

        // Create bookmarks for pages 5, 6, and 7 using the correct API.
        editor.CreateBookmarkOfPage("Part 1", 5);
        editor.CreateBookmarkOfPage("Part 2", 6);
        editor.CreateBookmarkOfPage("Part 3", 7);

        // Save the PDF with the new bookmarks.
        editor.Save(outputPath);
        editor.Close(); // optional, releases resources.

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
