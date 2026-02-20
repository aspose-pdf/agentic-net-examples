using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Path to the PDF file
        const string pdfPath = "input.pdf";

        // Verify that the file exists before proceeding
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        // Create the PdfBookmarkEditor facade
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            // Load (bind) the PDF document
            bookmarkEditor.BindPdf(pdfPath);

            // Extract all bookmarks (including nested ones)
            Bookmarks rootBookmarks = bookmarkEditor.ExtractBookmarks();

            // Print the bookmark hierarchy to the console
            PrintBookmarks(rootBookmarks, 0);
        }
    }

    // Recursively prints bookmarks with indentation representing hierarchy levels
    static void PrintBookmarks(Bookmarks bookmarks, int level)
    {
        if (bookmarks == null) return;

        foreach (Bookmark bm in bookmarks)
        {
            Console.WriteLine($"{new string(' ', level * 2)}- {bm.Title}");

            // If the bookmark has child items, process them recursively
            if (bm.ChildItems != null && bm.ChildItems.Count > 0)
            {
                PrintBookmarks(bm.ChildItems, level + 1);
            }
        }
    }
}