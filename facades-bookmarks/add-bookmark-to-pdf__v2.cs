using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfBookmarkEditor

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_bookmarked.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Use PdfBookmarkEditor to add a bookmark
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            // Bind the PDF document
            bookmarkEditor.BindPdf(inputPath);

            // Create a bookmark titled "Methodology" that points to page 20 (1‑based)
            // The correct API is CreateBookmarkOfPage, not a Bookmark constructor.
            bookmarkEditor.CreateBookmarkOfPage("Methodology", 20);

            // Save the modified PDF
            bookmarkEditor.Save(outputPath);
        }

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}
