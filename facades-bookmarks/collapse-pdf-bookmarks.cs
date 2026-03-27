using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_collapsed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the bookmark editor and bind the source PDF
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPath);

        // Retrieve all bookmarks from the document
        Bookmarks allBookmarks = editor.ExtractBookmarks();

        // Titles of bookmarks that should be collapsed (closed)
        string[] titlesToCollapse = new string[] { "Chapter 1", "Section A" };

        // Iterate through the bookmarks and set the Open property to false for matching titles
        foreach (Bookmark bm in allBookmarks)
        {
            foreach (string targetTitle in titlesToCollapse)
            {
                if (bm.Title == targetTitle)
                {
                    bm.Open = false; // collapsed state
                }
            }
        }

        // Save the modified PDF and release resources
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Collapsed bookmarks saved to '{outputPath}'.");
    }
}
