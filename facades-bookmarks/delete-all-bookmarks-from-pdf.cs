using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_bookmarks.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, delete all bookmarks, and save the result
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPath);   // load PDF into the facade
            editor.DeleteBookmarks();    // remove every bookmark
            editor.Save(outputPath);     // persist changes
        }

        Console.WriteLine($"All bookmarks deleted. Saved to '{outputPath}'.");
    }
}