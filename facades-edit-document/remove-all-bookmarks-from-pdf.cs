using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_bookmarks.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        try
        {
            // PdfBookmarkEditor provides bookmark manipulation without needing a Document instance
            PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

            // Load the PDF to be edited
            bookmarkEditor.BindPdf(inputPath);

            // Delete all existing bookmarks
            bookmarkEditor.DeleteBookmarks();

            // Save the modified PDF
            bookmarkEditor.Save(outputPath);

            Console.WriteLine($"All bookmarks removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}