using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string bookmarkTitle = "Chapter 1";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfBookmarkEditor (a facade) to add a bookmark.
        // The facade implements IDisposable, so wrap it in a using block.
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Load the existing PDF file.
            editor.BindPdf(inputPath);

            // Create a bookmark that points to page 1 (Aspose.Pdf uses 1‑based page indexing).
            editor.CreateBookmarkOfPage(bookmarkTitle, 1);

            // Save the modified PDF to a new file.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}