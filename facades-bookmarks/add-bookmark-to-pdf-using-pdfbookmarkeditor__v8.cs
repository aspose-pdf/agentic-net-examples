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

        // Verify the source PDF exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfBookmarkEditor does not implement IDisposable, so we instantiate it directly.
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

        // Bind the existing PDF to the editor.
        bookmarkEditor.BindPdf(inputPath);

        // Add a bookmark titled "Introduction" that points to page 2 (pages are 1‑based).
        // Use the correct method from the editor's surface.
        bookmarkEditor.CreateBookmarkOfPage("Introduction", 2);

        // Save the updated PDF to a new file.
        bookmarkEditor.Save(outputPath);
        bookmarkEditor.Close();

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}
