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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the bookmark editor facade
        PdfBookmarkEditor editor = new PdfBookmarkEditor();

        // Load the PDF document into the editor
        editor.BindPdf(inputPath);

        // Remove all bookmarks from the document
        editor.DeleteBookmarks();

        // Save the modified PDF to a new file
        editor.Save(outputPath);

        // Release resources held by the facade
        editor.Close();

        Console.WriteLine($"All bookmarks removed. Saved to '{outputPath}'.");
    }
}