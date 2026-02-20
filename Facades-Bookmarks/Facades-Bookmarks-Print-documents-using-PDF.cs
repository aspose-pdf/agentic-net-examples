using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Path to the PDF file to be printed.
        const string pdfPath = "sample.pdf";

        // Verify that the PDF file exists before proceeding.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // ---------- Print the PDF document ----------
            // PdfViewer is a facade that can render and print PDF files.
            using (PdfViewer viewer = new PdfViewer())
            {
                // Load the PDF file into the viewer.
                viewer.BindPdf(pdfPath);

                // Print the document to the default printer.
                // If a specific printer is required, use viewer.PrintDocument("PrinterName");
                viewer.PrintDocument();
            }

            // ---------- Optional: List all bookmarks ----------
            // Demonstrates how to extract and display bookmarks using PdfBookmarkEditor.
            using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
            {
                bookmarkEditor.BindPdf(pdfPath);

                // Extract all bookmarks (including nested ones).
                var allBookmarks = bookmarkEditor.ExtractBookmarks();

                Console.WriteLine("Bookmarks in the document:");
                PrintBookmarks(allBookmarks, 0);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    // Recursive helper to display bookmark titles with indentation.
    static void PrintBookmarks(Aspose.Pdf.Facades.Bookmarks bookmarks, int level)
    {
        if (bookmarks == null) return;

        foreach (Aspose.Pdf.Facades.Bookmark bm in bookmarks)
        {
            string indent = new string(' ', level * 2);
            Console.WriteLine($"{indent}- {bm.Title}");

            // Process child bookmarks, if any.
            if (bm.ChildItems != null && bm.ChildItems.Count > 0)
            {
                PrintBookmarks(bm.ChildItems, level + 1);
            }
        }
    }
}