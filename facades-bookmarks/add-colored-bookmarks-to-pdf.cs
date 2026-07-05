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

        // Initialize the bookmark editor and bind the source PDF
        var editor = new Aspose.Pdf.Facades.PdfBookmarkEditor();
        editor.BindPdf(inputPath);

        // ----- Create a warning bookmark (red) -----
        var warningBookmark = new Aspose.Pdf.Facades.Bookmark
        {
            Title      = "Warning Section",
            PageNumber = 2, // adjust to actual page number
            TitleColor = System.Drawing.Color.Red,
            BoldFlag   = true,
            ItalicFlag = false
        };

        // ----- Create an informational bookmark (green) -----
        var infoBookmark = new Aspose.Pdf.Facades.Bookmark
        {
            Title      = "Information Section",
            PageNumber = 3, // adjust to actual page number
            TitleColor = System.Drawing.Color.Green,
            BoldFlag   = false,
            ItalicFlag = false
        };

        // Add the bookmarks to the document
        editor.CreateBookmarks(warningBookmark);
        editor.CreateBookmarks(infoBookmark);

        // Save the modified PDF
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}