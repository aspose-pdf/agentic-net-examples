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
        const string bookmarkTitle = "Draft Outline";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF, delete the specified bookmark, and save the result.
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPath);
            editor.DeleteBookmarks(bookmarkTitle);
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmark \"{bookmarkTitle}\" removed. Saved to \"{outputPath}\".");
    }
}