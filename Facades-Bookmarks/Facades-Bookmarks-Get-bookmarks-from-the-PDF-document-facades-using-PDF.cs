using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Generic input file name as per guidelines
        const string pdfPath = "input.pdf";

        // Verify that the file exists before proceeding
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // Use the PdfBookmarkEditor facade to work with bookmarks
            using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
            {
                // Load the PDF document into the facade
                bookmarkEditor.BindPdf(pdfPath);

                // Extract the complete bookmark hierarchy (returns a collection)
                var bookmarks = bookmarkEditor.ExtractBookmarks();

                if (bookmarks == null || bookmarks.Count == 0)
                {
                    Console.WriteLine("No bookmarks found in the document.");
                }
                else
                {
                    // Iterate over top‑level bookmarks and print recursively
                    foreach (Bookmark bm in bookmarks)
                    {
                        PrintBookmark(bm, 0);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF bookmarks: {ex.Message}");
        }
    }

    // Recursively prints a bookmark and its children with indentation
    static void PrintBookmark(Bookmark bm, int level)
    {
        string indent = new string(' ', level * 2);
        Console.WriteLine($"{indent}- {bm.Title}");

        if (bm.ChildItems != null && bm.ChildItems.Count > 0)
        {
            foreach (Bookmark child in bm.ChildItems)
            {
                PrintBookmark(child, level + 1);
            }
        }
    }
}
