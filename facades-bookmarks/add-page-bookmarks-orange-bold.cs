using System;
using System.IO;
using System.Drawing;
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

        // PdfBookmarkEditor implements IDisposable, so wrap it in a using block.
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            // Bind the PDF file to the editor.
            bookmarkEditor.BindPdf(inputPath);

            // Create a bookmark for every page with orange color and bold style.
            // The overload CreateBookmarks(Color, bool boldFlag, bool italicFlag) generates
            // a bookmark for each page in the document.
            bookmarkEditor.CreateBookmarks(Color.Orange, true, false);

            // Save the PDF with the new bookmarks.
            bookmarkEditor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
