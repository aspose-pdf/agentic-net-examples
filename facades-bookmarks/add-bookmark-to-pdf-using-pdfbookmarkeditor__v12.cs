using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Instantiate the bookmark editor (PdfBookmarkEditor does NOT implement IDisposable)
        var bookmarkEditor = new PdfBookmarkEditor();

        // Bind the existing PDF document to the editor
        bookmarkEditor.BindPdf(inputPath);

        // Create a bookmark titled "Analysis" that points to page 14.
        // The facade API provides CreateBookmarkOfPage or CreateBookmarks(Bookmark).
        // Here we use the simplest overload:
        bookmarkEditor.CreateBookmarkOfPage("Analysis", 14);

        // Save the modified PDF to the specified output file
        bookmarkEditor.Save(outputPath);

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}
