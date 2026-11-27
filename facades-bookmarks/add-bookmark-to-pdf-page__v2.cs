using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "bookmarked_output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfBookmarkEditor does NOT implement IDisposable, so we manage it manually
        var bookmarkEditor = new PdfBookmarkEditor();

        // Load (bind) the PDF document
        bookmarkEditor.BindPdf(inputPath);

        // Create a bookmark titled "Details" that points to page 9 (1‑based indexing)
        // Use the correct method provided by PdfBookmarkEditor
        bookmarkEditor.CreateBookmarkOfPage("Details", 9);

        // Save the modified PDF
        bookmarkEditor.Save(outputPath);
        bookmarkEditor.Close();

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}
