using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // needed for Bookmark types if you extend the code

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string bookmarkTitle = "Draft Outline";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfBookmarkEditor does not implement IDisposable, so we instantiate it directly.
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

        // Bind the PDF document.
        bookmarkEditor.BindPdf(inputPath);

        // Delete the bookmark with the specified title.
        // The correct method is DeleteBookmarks(string title).
        bookmarkEditor.DeleteBookmarks(bookmarkTitle);

        // Save the modified PDF.
        bookmarkEditor.Save(outputPath);

        // Release resources held by the editor.
        bookmarkEditor.Close();

        Console.WriteLine($"Bookmark '{bookmarkTitle}' removed. Saved to '{outputPath}'.");
    }
}
