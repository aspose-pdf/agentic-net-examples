using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string bookmarkTitleToDelete = "Chapter 2";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the PDF file to the bookmark editor facade
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPdf);

        // List all bookmarks before deletion
        Console.WriteLine("Bookmarks before deletion:");
        Bookmarks before = editor.ExtractBookmarks();
        foreach (Bookmark bm in before)
        {
            Console.WriteLine($"- {bm.Title}");
        }

        // Delete the bookmark with the specified title
        editor.DeleteBookmarks(bookmarkTitleToDelete);

        // List remaining bookmarks to verify removal
        Console.WriteLine("\nBookmarks after deletion:");
        Bookmarks after = editor.ExtractBookmarks();
        foreach (Bookmark bm in after)
        {
            Console.WriteLine($"- {bm.Title}");
        }

        // Save the modified PDF
        editor.Save(outputPdf);

        // Clean up resources
        editor.Close();
    }
}