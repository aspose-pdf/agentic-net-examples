using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify the input PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the bookmark editor and bind the PDF file
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPath);

        // Extract all bookmarks (recursive)
        Bookmarks bookmarks = editor.ExtractBookmarks();

        // Output each bookmark title
        foreach (Bookmark bm in bookmarks)
        {
            Console.WriteLine(bm.Title);
        }

        // Release resources held by the editor
        editor.Close();
    }
}