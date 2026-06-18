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

        // Load the PDF document into the editor
        editor.BindPdf(inputPath);

        // Remove all bookmarks from the document
        editor.DeleteBookmarks();

        // Save the resulting PDF without bookmarks
        editor.Save(outputPath);

        // Release any resources held by the facade
        editor.Close();

        Console.WriteLine($"All bookmarks removed. Saved to '{outputPath}'.");
    }
}