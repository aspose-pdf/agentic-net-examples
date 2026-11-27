using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing; // for System.Drawing.Color

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

        try
        {
            // PdfBookmarkEditor does not implement IDisposable, so we instantiate it directly.
            var bookmarkEditor = new PdfBookmarkEditor();

            // Bind the source PDF file.
            bookmarkEditor.BindPdf(inputPath);

            // Create orange‑colored bold bookmarks for every page.
            // The CreateBookmarks overload expects a System.Drawing.Color.
            bookmarkEditor.CreateBookmarks(System.Drawing.Color.Orange, boldFlag: true, italicFlag: false);

            // Save the PDF with the new bookmarks.
            bookmarkEditor.Save(outputPath);

            Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
