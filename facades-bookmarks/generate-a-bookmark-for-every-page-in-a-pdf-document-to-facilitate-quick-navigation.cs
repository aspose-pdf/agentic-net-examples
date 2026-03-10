using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_bookmarked.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the bookmark editor and bind the PDF document
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPath);

        // Create a bookmark for every page in the document
        editor.CreateBookmarks();

        // Save the PDF with the newly added bookmarks
        editor.Save(outputPath);

        Console.WriteLine($"Bookmarks added for all pages. Saved to '{outputPath}'.");
    }
}