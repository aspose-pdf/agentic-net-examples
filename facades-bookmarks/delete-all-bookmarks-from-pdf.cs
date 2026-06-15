using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_bookmarks.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Delete all bookmarks using PdfBookmarkEditor (facade API)
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPath);   // load the PDF
            editor.DeleteBookmarks();    // remove every bookmark
            editor.Save(outputPath);     // save the modified PDF
        }

        Console.WriteLine($"All bookmarks deleted. Saved to '{outputPath}'.");
    }
}