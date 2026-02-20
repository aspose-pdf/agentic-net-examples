using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF file
        const string pdfPath = "input.pdf";

        // Verify that the file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Create the PdfBookmarkEditor facade
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            // Load the PDF document
            bookmarkEditor.BindPdf(pdfPath);

            // Extract all bookmarks (including nested ones)
            var allBookmarks = bookmarkEditor.ExtractBookmarks();

            // Print the bookmark hierarchy to the console
            PrintBookmarks(allBookmarks, 0);
        }
    }

    // Recursive method to display bookmarks with indentation
    static void PrintBookmarks(Aspose.Pdf.Facades.Bookmarks bookmarks, int indentLevel)
    {
        if (bookmarks == null) return;

        foreach (Aspose.Pdf.Facades.Bookmark bm in bookmarks)
        {
            Console.WriteLine($"{new string(' ', indentLevel * 2)}- {bm.Title}");

            // Recursively process child bookmarks, if any
            if (bm.ChildItems != null && bm.ChildItems.Count > 0)
            {
                PrintBookmarks(bm.ChildItems, indentLevel + 1);
            }
        }
    }
}