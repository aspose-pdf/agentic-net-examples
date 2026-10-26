using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_bookmarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the bookmark editor (PdfBookmarkEditor does NOT implement IDisposable)
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPath);

        // Extract existing bookmarks
        Bookmarks bookmarks = editor.ExtractBookmarks();

        // Update each bookmark to open at 150% zoom (XYZ destination with Zoom = 150)
        foreach (Bookmark bm in bookmarks)
        {
            bm.PageDisplay = "XYZ";          // specify XYZ destination type
            bm.PageDisplay_Zoom = 150;        // zoom expressed as a percentage
        }

        // Remove the old bookmarks and add the modified ones back
        editor.DeleteBookmarks();
        foreach (Bookmark bm in bookmarks)
        {
            editor.CreateBookmarks(bm);
        }

        // Save the modified PDF
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Bookmarks updated to 150% zoom and saved to '{outputPath}'.");
    }
}
