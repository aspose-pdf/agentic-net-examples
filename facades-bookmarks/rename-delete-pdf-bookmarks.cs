using System;
using System.IO;
using Aspose.Pdf.Facades;

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

        // PdfBookmarkEditor does not implement IDisposable, so a using block is not required.
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

        // Bind the PDF document to the editor.
        bookmarkEditor.BindPdf(inputPath);

        // Rename the bookmark titled "OldSection" to "NewSection".
        // The correct method for renaming is ModifyBookmarks(sourceTitle, destinationTitle).
        bookmarkEditor.ModifyBookmarks("OldSection", "NewSection");

        // Delete the bookmark titled "Obsolete".
        // The overload that accepts a single string deletes by title.
        bookmarkEditor.DeleteBookmarks("Obsolete");

        // Save the modified PDF.
        bookmarkEditor.Save(outputPath);

        Console.WriteLine($"Bookmarks updated and saved to '{outputPath}'.");
    }
}
