using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "cleaned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfBookmarkEditor does not implement IDisposable, so no using block is needed.
            PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

            // Load the PDF document into the editor.
            bookmarkEditor.BindPdf(inputPath);

            // Delete all existing bookmarks.
            bookmarkEditor.DeleteBookmarks();

            // Save the PDF without bookmarks.
            bookmarkEditor.Save(outputPath);

            Console.WriteLine($"Bookmarks removed. Clean PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}