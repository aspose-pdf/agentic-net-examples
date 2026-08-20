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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create the bookmark editor facade
        PdfBookmarkEditor editor = new PdfBookmarkEditor();

        try
        {
            // Load the PDF into the facade
            editor.BindPdf(inputPath);

            // Remove all bookmarks from the document
            editor.DeleteBookmarks();

            // Persist the changes to a new file
            editor.Save(outputPath);
        }
        finally
        {
            // Ensure resources are released
            editor.Close();
        }

        Console.WriteLine($"All bookmarks removed. Output saved to '{outputPath}'.");
    }
}