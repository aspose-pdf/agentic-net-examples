using System;
using System.IO;
using Aspose.Pdf;
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

        // Load the PDF to obtain the page count.
        int pageCount;
        using (Document doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count;
        }

        // PdfBookmarkEditor does NOT implement IDisposable.
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

        // Bind the editor to the source PDF.
        bookmarkEditor.BindPdf(inputPath);

        // Create a bookmark for each page using the correct API.
        for (int i = 1; i <= pageCount; i++)
        {
            string title = $"Page {i}";
            // The method that actually creates a bookmark for a page is CreateBookmarkOfPage.
            bookmarkEditor.CreateBookmarkOfPage(title, i);
        }

        // Save the updated PDF with the new bookmarks.
        bookmarkEditor.Save(outputPath);
        bookmarkEditor.Close();

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
