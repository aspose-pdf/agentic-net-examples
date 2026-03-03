using System;
using System.IO;
using Aspose.Pdf.Facades; // Contains PdfBookmarkEditor, Bookmark, Bookmarks

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the bookmark editor and bind the PDF file
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPdf); // Load PDF into the facade

        // Extract all bookmarks (recursive)
        Bookmarks bookmarks = editor.ExtractBookmarks();

        // Output bookmark titles and their target page numbers
        foreach (Bookmark bm in bookmarks)
        {
            Console.WriteLine($"Title: {bm.Title}, Page: {bm.PageNumber}");
        }

        // Release resources held by the editor
        editor.Close();
    }
}