using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing; // Required for bookmark color (System.Drawing.Color)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "bookmarked_output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use PdfBookmarkEditor to add bookmarks for all pages
        // The editor implements IDisposable, so wrap it in a using block
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Bind the source PDF file
            editor.BindPdf(inputPath);

            // Create bookmarks for every page with specified properties:
            //   - Color: Blue
            //   - Bold:  true
            //   - Italic:false
            editor.CreateBookmarks(Color.Blue, true, false);

            // Save the modified PDF with bookmarks
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}