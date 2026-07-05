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
        const string bookmarkTitle = "Draft Outline";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the bookmark editor, bind the PDF, delete the specific bookmark, and save.
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPath);                     // Load the PDF file.
            editor.DeleteBookmarks(bookmarkTitle);         // Remove the bookmark titled "Draft Outline".
            editor.Save(outputPath);                       // Persist changes to a new file.
        }

        Console.WriteLine($"Bookmark \"{bookmarkTitle}\" removed. Saved to '{outputPath}'.");
    }
}