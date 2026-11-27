using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_bookmark.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Determine the last page number (1‑based indexing)
        int lastPageNumber;
        using (Document doc = new Document(inputPath))
        {
            lastPageNumber = doc.Pages.Count;
        }

        // Create the bookmark editor, bind the PDF, add the bookmark, and save.
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            bookmarkEditor.BindPdf(inputPath);

            // Create a bookmark titled "References" that points to the last page.
            bookmarkEditor.CreateBookmarkOfPage("References", lastPageNumber);

            // Save the modified PDF.
            bookmarkEditor.Save(outputPath);
            // Optional: close the editor (Dispose will also close).
            bookmarkEditor.Close();
        }

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}
