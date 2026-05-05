using System;
using System.IO;
using System.Drawing;               // Required for bookmark color
using Aspose.Pdf.Facades;          // Facade API for bookmark manipulation

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the bookmark editor and bind the source PDF
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPath);

        // Create a top‑level bookmark that points to page 1
        Bookmark bookmark = new Bookmark
        {
            Title       = "First Page",
            PageNumber  = 1,
            TitleColor  = Color.Blue   // Set bookmark title color to blue
        };

        // Add the bookmark to the document
        editor.CreateBookmarks(bookmark);

        // Save the modified PDF
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Bookmark added and saved to '{outputPath}'.");
    }
}