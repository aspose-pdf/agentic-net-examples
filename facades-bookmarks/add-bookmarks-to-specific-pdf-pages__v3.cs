using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "bookmarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfBookmarkEditor implements IDisposable, so use a using block.
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Bind the existing PDF document.
            editor.BindPdf(inputPath);

            // Create individual bookmarks for pages 6, 7 and 8.
            editor.CreateBookmarkOfPage("Section X", 6);
            editor.CreateBookmarkOfPage("Section Y", 7);
            editor.CreateBookmarkOfPage("Section Z", 8);

            // Save the PDF with the new bookmarks.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
