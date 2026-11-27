using System;
using System.IO;
using System.Drawing; // for System.Drawing.Color
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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the editor
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();
        bookmarkEditor.BindPdf(inputPath);

        // Create regular (non‑outline) bookmarks for all pages, red colour, not bold, not italic
        // The overload CreateBookmarks(Color, bool boldFlag, bool italicFlag) generates a bookmark per page.
        bookmarkEditor.CreateBookmarks(System.Drawing.Color.Red, false, false);

        // Save the PDF with the new bookmarks
        bookmarkEditor.Save(outputPath);

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
