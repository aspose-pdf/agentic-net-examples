using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_bookmark.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfBookmarkEditor does not implement IDisposable in this version.
        PdfBookmarkEditor editor = new PdfBookmarkEditor();

        // Bind the existing PDF.
        editor.BindPdf(inputPath);

        // Create a bookmark titled "Details" that points to page 15 (1‑based).
        editor.CreateBookmarkOfPage("Details", 15);

        // Save the modified PDF.
        editor.Save(outputPath);

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}
