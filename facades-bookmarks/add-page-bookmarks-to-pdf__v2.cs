using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "bookmarked_output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        try
        {
            // PdfBookmarkEditor does NOT implement IDisposable, so we instantiate it directly
            var bookmarkEditor = new PdfBookmarkEditor();

            // Bind the existing PDF file to the editor
            bookmarkEditor.BindPdf(inputPath);

            // Create bookmarks for pages 2, 3 and 4 using the correct API method
            bookmarkEditor.CreateBookmarkOfPage("Section A", 2);
            bookmarkEditor.CreateBookmarkOfPage("Section B", 3);
            bookmarkEditor.CreateBookmarkOfPage("Section C", 4);

            // Save the modified PDF with the new bookmarks
            bookmarkEditor.Save(outputPath);
            bookmarkEditor.Close();

            Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
