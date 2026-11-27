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

        // Bind the source PDF to the bookmark editor
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();
        bookmarkEditor.BindPdf(inputPath);

        // Create magenta coloured regular (non‑bold, non‑italic) bookmarks for every page
        // The overload CreateBookmarks(System.Drawing.Color, bool, bool) generates a bookmark per page.
        bookmarkEditor.CreateBookmarks(System.Drawing.Color.Magenta, false, false);

        // Save the PDF with the new bookmarks
        bookmarkEditor.Save(outputPath);
        bookmarkEditor.Close();

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
