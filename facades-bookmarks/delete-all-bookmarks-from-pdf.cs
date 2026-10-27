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

        // PdfBookmarkEditor does not implement IDisposable, so no using block is needed.
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

        // Bind the source PDF, delete all its bookmarks, and save the result to a new file.
        bookmarkEditor.BindPdf(inputPath);
        bookmarkEditor.DeleteBookmarks();
        bookmarkEditor.Save(outputPath);

        Console.WriteLine($"All bookmarks removed. Saved to '{outputPath}'.");
    }
}
