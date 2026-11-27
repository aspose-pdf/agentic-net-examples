using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_bookmarked.pdf";

        // Verify the source PDF exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfBookmarkEditor does not implement IDisposable, so we instantiate it directly.
        var editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPath);

        // Create bookmarks for pages 3, 4, and 5 with the required titles.
        editor.CreateBookmarkOfPage("Part A", 3);
        editor.CreateBookmarkOfPage("Part B", 4);
        editor.CreateBookmarkOfPage("Part C", 5);

        // Save the PDF with the new bookmarks and close the editor.
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
