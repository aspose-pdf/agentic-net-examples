using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Input and output PDF file paths
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Load the source PDF document
        Document pdfDocument = new Document(inputPath);

        // Work with bookmarks using the Facades API
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();
        bookmarkEditor.BindPdf(pdfDocument);

        // Extract the bookmarks collection
        Bookmarks bookmarks = bookmarkEditor.ExtractBookmarks();

        // Print bookmark hierarchy to the console
        PrintBookmarks(bookmarks, 0);

        // Save the (possibly modified) document to the output path
        pdfDocument.Save(outputPath);
        Console.WriteLine($"PDF successfully saved to '{outputPath}'.");
    }

    // Recursive helper to display bookmarks with indentation
    static void PrintBookmarks(Bookmarks collection, int level)
    {
        if (collection == null) return;

        foreach (Bookmark bm in collection)
        {
            Console.WriteLine($"{new string(' ', level * 2)}- {bm.Title}");
            if (bm.ChildItems != null && bm.ChildItems.Count > 0)
            {
                PrintBookmarks(bm.ChildItems, level + 1);
            }
        }
    }
}