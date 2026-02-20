using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source PDF file
        const string pdfPath = "input.pdf";

        // Verify that the PDF file exists before processing
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        // Initialize the PdfBookmarkEditor facade and bind the PDF document
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            bookmarkEditor.BindPdf(pdfPath);

            // Extract all bookmarks (including nested ones) from the document
            Bookmarks rootBookmarks = bookmarkEditor.ExtractBookmarks();

            // Output the bookmark hierarchy to the console
            PrintBookmarks(rootBookmarks, 0);
        }
    }

    // Recursively prints bookmarks with indentation reflecting their hierarchy level
    static void PrintBookmarks(Bookmarks bookmarks, int level)
    {
        if (bookmarks == null) return;

        foreach (Bookmark bm in bookmarks)
        {
            string indent = new string(' ', level * 2);
            Console.WriteLine($"{indent}Title: {bm.Title}");

            // Destination is a string that may contain page reference or explicit destination
            if (!string.IsNullOrEmpty(bm.Destination))
            {
                Console.WriteLine($"{indent} Destination: {bm.Destination}");
            }

            // Process child bookmarks, if any
            if (bm.ChildItems != null && bm.ChildItems.Count > 0)
            {
                PrintBookmarks(bm.ChildItems, level + 1);
            }
        }
    }
}