using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_bookmarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the bookmark editor facade
        PdfBookmarkEditor editor = new PdfBookmarkEditor();

        // Load the PDF document into the facade
        editor.BindPdf(inputPath);

        // Create a bookmark for each page in the document
        editor.CreateBookmarks();

        // Save the PDF with the newly added bookmarks
        editor.Save(outputPath);

        // Release resources held by the facade
        editor.Close();

        Console.WriteLine($"Bookmarks added successfully. Saved to '{outputPath}'.");
    }
}