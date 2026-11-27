using System;
using System.IO;
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

        try
        {
            // PdfBookmarkEditor does not implement IDisposable, but we must close it explicitly.
            var bookmarkEditor = new PdfBookmarkEditor();

            // Load the PDF document.
            bookmarkEditor.BindPdf(inputPath);

            // Create a bookmark that points to page 3.
            var overviewBookmark = new Bookmark
            {
                Title = "Overview",
                PageNumber = 3,
                // The facade Bookmark expects the action as a string.
                Action = "GoTo"
            };

            // Add the bookmark to the document using the correct method.
            bookmarkEditor.CreateBookmarks(overviewBookmark);

            // Save the modified PDF.
            bookmarkEditor.Save(outputPath);
            bookmarkEditor.Close();

            Console.WriteLine($"Bookmark added and PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
