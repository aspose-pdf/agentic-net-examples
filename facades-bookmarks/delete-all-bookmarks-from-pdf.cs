using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_bookmarks.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the bookmark editor facade
        PdfBookmarkEditor editor = new PdfBookmarkEditor();

        try
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Delete all bookmarks from the document
            editor.DeleteBookmarks();

            // Save the modified PDF
            editor.Save(outputPath);
        }
        finally
        {
            // Release resources held by the facade
            editor.Close();
        }

        Console.WriteLine($"All bookmarks removed. Saved to '{outputPath}'.");
    }
}