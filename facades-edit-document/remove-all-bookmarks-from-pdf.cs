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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the bookmark editor facade
        PdfBookmarkEditor editor = new PdfBookmarkEditor();

        // Load the PDF document
        editor.BindPdf(inputPath);

        // Remove all bookmarks
        editor.DeleteBookmarks();

        // Save the updated PDF
        editor.Save(outputPath);

        // Release resources held by the facade
        editor.Close();

        Console.WriteLine($"All bookmarks removed. Saved to '{outputPath}'.");
    }
}