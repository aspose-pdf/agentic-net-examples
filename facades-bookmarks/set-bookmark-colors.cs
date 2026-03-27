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

        // Initialize the bookmark editor and bind the source PDF
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPath);

            // Create a bookmark for a warning section (red title)
            Bookmark warningBookmark = new Bookmark();
            warningBookmark.Title = "Warnings";
            warningBookmark.PageNumber = 1;
            warningBookmark.TitleColor = System.Drawing.Color.Red;
            warningBookmark.BoldFlag = true;
            editor.CreateBookmarks(warningBookmark);

            // Create a bookmark for an informational section (green title)
            Bookmark infoBookmark = new Bookmark();
            infoBookmark.Title = "Information";
            infoBookmark.PageNumber = 2;
            infoBookmark.TitleColor = System.Drawing.Color.Green;
            infoBookmark.BoldFlag = false;
            editor.CreateBookmarks(infoBookmark);

            // Save the modified PDF with colored bookmarks
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks with colors saved to '{outputPath}'.");
    }
}