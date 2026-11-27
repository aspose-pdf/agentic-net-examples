using System;
using System.Drawing;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "bookmarked_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF file to the editor
        var bookmarkEditor = new PdfBookmarkEditor();
        bookmarkEditor.BindPdf(inputPath);

        // Create bookmarks for every page with both Bold and Italic style.
        // The overload CreateBookmarks(Color, bool boldFlag, bool italicFlag) creates
        // a bookmark for each page in the document using the supplied style flags.
        // Using Color.Black (default) – you can change the colour if desired.
        bookmarkEditor.CreateBookmarks(Color.Black, true, true);

        // Save the modified PDF with the new bookmarks
        bookmarkEditor.Save(outputPath);
        bookmarkEditor.Close();

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
