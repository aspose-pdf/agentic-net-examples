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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // CREATE: instantiate the bookmark editor
        PdfBookmarkEditor editor = new PdfBookmarkEditor();

        try
        {
            // LOAD: bind the PDF document to the editor
            editor.BindPdf(inputPath);

            // OPERATION: delete all bookmarks
            editor.DeleteBookmarks();

            // SAVE: write the modified PDF to a new file
            editor.Save(outputPath);
        }
        finally
        {
            // RELEASE: close the editor and free resources
            editor.Close();
        }

        Console.WriteLine($"All bookmarks removed. Saved to '{outputPath}'.");
    }
}