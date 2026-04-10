using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF that contains bookmarks
        const string inputPdf = "input.pdf";
        // Output PDF after the bookmark is removed
        const string outputPdf = "output_without_bookmark.pdf";
        // Title of the bookmark to delete
        const string bookmarkTitle = "Bookmark To Delete";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // ---------- Delete the specified bookmark ----------
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPdf);
        // Delete the bookmark with the given title
        editor.DeleteBookmarks(bookmarkTitle);
        // Save the modified PDF
        editor.Save(outputPdf);
        // Release resources held by the facade
        editor.Close();

        // ---------- Verify removal by extracting remaining bookmarks ----------
        PdfBookmarkEditor verifier = new PdfBookmarkEditor();
        verifier.BindPdf(outputPdf);
        // Extract all bookmarks from the updated document
        Bookmarks remaining = verifier.ExtractBookmarks();

        bool found = false;
        Console.WriteLine("Remaining bookmarks after deletion:");
        foreach (Bookmark bm in remaining)
        {
            Console.WriteLine($"- {bm.Title}");
            if (string.Equals(bm.Title, bookmarkTitle, StringComparison.OrdinalIgnoreCase))
                found = true;
        }

        if (found)
            Console.WriteLine($"Error: Bookmark \"{bookmarkTitle}\" was not removed.");
        else
            Console.WriteLine($"Success: Bookmark \"{bookmarkTitle}\" has been removed.");

        verifier.Close();
    }
}