using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_bookmarks.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfBookmarkEditor implements IDisposable, so wrap it in a using block
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Load the PDF file into the editor
            editor.BindPdf(inputPath);

            // Create a bookmark for every page in the document
            editor.CreateBookmarks();

            // Save the PDF with the newly added bookmarks
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added successfully. Saved to '{outputPath}'.");
    }
}