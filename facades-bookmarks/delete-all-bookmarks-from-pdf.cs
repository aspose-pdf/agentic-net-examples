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

        // Create the bookmark editor
        PdfBookmarkEditor editor = new PdfBookmarkEditor();

        // Load the PDF document
        editor.BindPdf(inputPath);

        // Delete all bookmarks
        editor.DeleteBookmarks();

        // Save the modified PDF
        editor.Save(outputPath);

        // Release resources
        editor.Close();

        Console.WriteLine($"All bookmarks removed. Saved to '{outputPath}'.");
    }
}