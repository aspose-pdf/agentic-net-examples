using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_bookmarks.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the bookmark editor and bind the PDF
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        try
        {
            editor.BindPdf(inputPath);          // Load the PDF
            editor.DeleteBookmarks();           // Remove all bookmarks
            editor.Save(outputPath);            // Save the result
        }
        finally
        {
            // Ensure resources are released
            editor.Close();
        }

        Console.WriteLine($"All bookmarks removed. Saved to '{outputPath}'.");
    }
}