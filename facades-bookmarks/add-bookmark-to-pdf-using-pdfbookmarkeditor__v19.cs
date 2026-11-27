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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfBookmarkEditor does not implement IDisposable, so we instantiate it directly.
            var bookmarkEditor = new PdfBookmarkEditor();

            // Bind the existing PDF document.
            bookmarkEditor.BindPdf(inputPath);

            // Create a bookmark for page 15. The facade Bookmark has no (title, page) ctor;
            // we set its properties explicitly and specify the action as "GoTo".
            var conclusionBookmark = new Bookmark
            {
                Title = "Conclusion",
                PageNumber = 15,
                Action = "GoTo"
            };

            // Add the bookmark using the correct method.
            bookmarkEditor.CreateBookmarks(conclusionBookmark);

            // Save the modified PDF.
            bookmarkEditor.Save(outputPath);
            bookmarkEditor.Close();

            Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
