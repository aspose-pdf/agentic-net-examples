using System;
using System.IO;
using Aspose.Pdf;
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

        // PdfBookmarkEditor does not implement IDisposable, so we instantiate it directly.
        PdfBookmarkEditor editor = new PdfBookmarkEditor();

        // Bind the existing PDF file to the editor.
        editor.BindPdf(inputPath);

        // Create a bookmark that points to page 5.
        Bookmark intro = new Bookmark
        {
            Title = "Introduction",
            PageNumber = 5,
            // The Action property must be a string; "GoTo" tells the viewer to navigate to the page.
            Action = "GoTo"
        };

        // Add the bookmark to the document using the correct method.
        editor.CreateBookmarks(intro);

        // Save the modified PDF with the new bookmark.
        editor.Save(outputPath);

        // Release any resources held by the editor.
        editor.Close();

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}
