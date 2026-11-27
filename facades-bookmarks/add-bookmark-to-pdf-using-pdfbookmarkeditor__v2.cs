using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfBookmarkEditor does not implement IDisposable, so we instantiate it directly.
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

        // Load the PDF document into the editor.
        bookmarkEditor.BindPdf(inputPath);

        // Add a bookmark titled "Appendix" that points to page 10.
        // The correct method on PdfBookmarkEditor is CreateBookmarkOfPage.
        bookmarkEditor.CreateBookmarkOfPage("Appendix", 10);

        // Save the modified PDF to a new file.
        bookmarkEditor.Save(outputPath);
        bookmarkEditor.Close();

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}
