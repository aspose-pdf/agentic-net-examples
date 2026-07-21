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
        const string bookmarkTitle = "My Bookmark";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the source PDF, delete the bookmark with the specified title, and save the result.
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPath);
        editor.DeleteBookmarks(bookmarkTitle);
        editor.Save(outputPath);
        editor.Close();

        // Verify that the bookmark has been removed by extracting remaining bookmarks.
        PdfBookmarkEditor verifier = new PdfBookmarkEditor();
        verifier.BindPdf(outputPath);
        Bookmarks remaining = verifier.ExtractBookmarks();
        Console.WriteLine("Remaining bookmarks after deletion:");
        foreach (Bookmark bm in remaining)
        {
            Console.WriteLine($"- {bm.Title}");
        }
        verifier.Close();
    }
}