using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_bookmarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfBookmarkEditor does not implement IDisposable, so we manage its lifetime manually.
        var bookmarkEditor = new PdfBookmarkEditor();
        bookmarkEditor.BindPdf(inputPath);

        // Create a top‑level bookmark titled "Project Overview" that navigates to page 5.
        // PdfBookmarkEditor provides CreateBookmarkOfPage for this purpose.
        bookmarkEditor.CreateBookmarkOfPage("Project Overview", 5);

        // Save the modified PDF and close the editor.
        bookmarkEditor.Save(outputPath);
        bookmarkEditor.Close();

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}
