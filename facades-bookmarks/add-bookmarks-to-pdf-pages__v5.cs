using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "bookmarked_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use a using block to ensure resources are released
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            // Load and bind the PDF document
            bookmarkEditor.BindPdf(inputPath);

            // Create bookmarks for the required pages (page numbers are 1‑based)
            bookmarkEditor.CreateBookmarkOfPage("Overview", 16);
            bookmarkEditor.CreateBookmarkOfPage("Summary", 21);

            // Save the PDF with the new bookmarks
            bookmarkEditor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
