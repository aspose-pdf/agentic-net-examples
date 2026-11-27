using System;
using System.IO;
using Aspose.Pdf;
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

        // Determine the last page number (1‑based indexing)
        int lastPageNumber;
        using (var doc = new Document(inputPath))
        {
            lastPageNumber = doc.Pages.Count;
        }

        // Use PdfBookmarkEditor to add a bookmark
        using (var bookmarkEditor = new PdfBookmarkEditor())
        {
            bookmarkEditor.BindPdf(inputPath);

            // Create a bookmark titled "Index" that points to the last page
            bookmarkEditor.CreateBookmarkOfPage("Index", lastPageNumber);

            // Save the modified PDF
            bookmarkEditor.Save(outputPath);
        }

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}
