using System;
using System.IO;
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

        // PdfBookmarkEditor does not implement IDisposable, so no using block is needed.
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

        // Bind the source PDF to the editor.
        bookmarkEditor.BindPdf(inputPath);

        // Create bookmarks for pages 10, 11, and 12 using the correct method.
        bookmarkEditor.CreateBookmarkOfPage("Module I", 10);
        bookmarkEditor.CreateBookmarkOfPage("Module II", 11);
        bookmarkEditor.CreateBookmarkOfPage("Module III", 12);

        // Save the PDF with the new bookmarks.
        bookmarkEditor.Save(outputPath);

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
