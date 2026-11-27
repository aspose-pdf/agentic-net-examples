using System;
using System.IO;
using Aspose.Pdf;
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

        // Determine the number of pages in the source PDF.
        int pageCount;
        using (Document doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count;
        }

        // PdfBookmarkEditor does not implement IDisposable, so we instantiate it directly.
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();
        // Bind the existing PDF file.
        bookmarkEditor.BindPdf(inputPath);

        // Create a simple bookmark for each page. The facade provides CreateBookmarkOfPage
        // which internally builds the required GoTo action – no need to reference GoToAction
        // or use an AddBookmark method (which does not exist).
        for (int i = 1; i <= pageCount; i++)
        {
            bookmarkEditor.CreateBookmarkOfPage($"Page {i}", i);
        }

        // Persist the changes.
        bookmarkEditor.Save(outputPath);

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
