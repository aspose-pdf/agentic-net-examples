using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF with bookmarks
        const string outputPdf = "output.pdf";         // PDF after deletion
        const string bookmarkTitle = "Bookmark To Delete"; // title of the bookmark to remove

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // ---------- Extract and display bookmarks before deletion ----------
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPdf);

        Console.WriteLine("Bookmarks before deletion:");
        PrintBookmarks(editor.ExtractBookmarks());

        // ---------- Delete the specified bookmark ----------
        editor.DeleteBookmarks(bookmarkTitle);
        editor.Save(outputPdf);
        editor.Close(); // release resources held by the facade

        // ---------- Verify removal by extracting bookmarks from the saved file ----------
        PdfBookmarkEditor verifyEditor = new PdfBookmarkEditor();
        verifyEditor.BindPdf(outputPdf);

        Console.WriteLine("\nBookmarks after deletion:");
        PrintBookmarks(verifyEditor.ExtractBookmarks());

        verifyEditor.Close();
    }

    // Helper method to enumerate and print bookmark titles
    static void PrintBookmarks(Bookmarks bookmarks)
    {
        if (bookmarks == null || bookmarks.Count == 0)
        {
            Console.WriteLine("  (none)");
            return;
        }

        foreach (Bookmark bm in bookmarks)
        {
            Console.WriteLine($"  - {bm.Title}");
        }
    }
}