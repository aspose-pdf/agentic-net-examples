using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string bookmarkTitle = "MyBookmark";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the bookmark editor
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPath);

            // Remove existing bookmark with the same title (if any)
            editor.DeleteBookmarks(bookmarkTitle);

            // Create a new bookmark that points to page 10
            editor.CreateBookmarkOfPage(bookmarkTitle, 10);

            // Save the updated PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmark '{bookmarkTitle}' now points to page 10 in '{outputPath}'.");
    }
}
