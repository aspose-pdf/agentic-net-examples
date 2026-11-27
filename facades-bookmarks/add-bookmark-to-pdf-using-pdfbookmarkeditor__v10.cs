using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // Bookmark class lives in this namespace

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string bookmarkTitle = "Acknowledgements";
        const int pageNumber = 3; // 1‑based page index

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // PdfBookmarkEditor does not implement IDisposable.
            var editor = new PdfBookmarkEditor();

            // Bind the existing PDF file.
            editor.BindPdf(inputPath);

            // Build a single Bookmark that points to page 3.
            var bm = new Bookmark
            {
                Title = bookmarkTitle,
                PageNumber = pageNumber,
                Action = "GoTo" // required action string for a page link
            };

            // Add the bookmark to the document.
            editor.CreateBookmarks(bm);

            // Save the modified PDF.
            editor.Save(outputPath);
            editor.Close();

            Console.WriteLine($"Bookmark \"{bookmarkTitle}\" added on page {pageNumber} and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
