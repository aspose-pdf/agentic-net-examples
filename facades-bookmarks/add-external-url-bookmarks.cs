using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the bookmark editor and bind the source PDF
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPath);

        // Create a bookmark that opens Google in a web browser
        Bookmark googleBookmark = new Bookmark();
        googleBookmark.Title = "Google";
        googleBookmark.Action = "URI"; // Action type for external URL
        googleBookmark.Destination = "https://www.google.com";

        // Create another bookmark that opens the Aspose PDF product page
        Bookmark asposeBookmark = new Bookmark();
        asposeBookmark.Title = "Aspose PDF";
        asposeBookmark.Action = "URI";
        asposeBookmark.Destination = "https://pdf.aspose.com";

        // Add the bookmarks to the document
        editor.CreateBookmarks(googleBookmark);
        editor.CreateBookmarks(asposeBookmark);

        // Save the updated PDF with the new bookmarks
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}