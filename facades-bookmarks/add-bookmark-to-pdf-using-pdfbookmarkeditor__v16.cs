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

        // PdfBookmarkEditor does not implement IDisposable, so we instantiate it directly.
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

        // Bind the existing PDF document.
        bookmarkEditor.BindPdf(inputPath);

        // Add a top‑level bookmark titled "Postscript" that points to page 20.
        // The correct method on PdfBookmarkEditor is CreateBookmarkOfPage, not AddBookmark.
        bookmarkEditor.CreateBookmarkOfPage("Postscript", 20);

        // Save the modified PDF.
        bookmarkEditor.Save(outputPath);

        // Optionally close the editor (not strictly required for saving).
        bookmarkEditor.Close();

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}
