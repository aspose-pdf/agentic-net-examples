using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_bookmarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Get total page count using Document (disposed via using)
        int pageCount;
        using (var doc = new Document(inputPath))
        {
            pageCount = doc.Pages.Count;
        }

        // PdfBookmarkEditor does not implement IDisposable
        var bookmarkEditor = new PdfBookmarkEditor();
        bookmarkEditor.BindPdf(inputPath);
        // Create a bookmark titled "End" on the last page
        bookmarkEditor.CreateBookmarkOfPage("End", pageCount);
        // Save the PDF with the new bookmark
        bookmarkEditor.Save(outputPath);
        // Release internal resources
        bookmarkEditor.Close();

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}
