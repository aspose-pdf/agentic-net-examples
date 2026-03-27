using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the original PDF and count its bookmarks
        int originalBookmarkCount;
        using (Document originalDoc = new Document(inputPath))
        {
            PdfBookmarkEditor originalEditor = new PdfBookmarkEditor();
            originalEditor.BindPdf(originalDoc);
            Bookmarks originalBookmarks = originalEditor.ExtractBookmarks();
            originalBookmarkCount = originalBookmarks.Count;

            // Save the document to a new file
            originalDoc.Save(outputPath);
        }

        // Load the saved PDF and count its bookmarks
        int savedBookmarkCount;
        using (Document savedDoc = new Document(outputPath))
        {
            PdfBookmarkEditor savedEditor = new PdfBookmarkEditor();
            savedEditor.BindPdf(savedDoc);
            Bookmarks savedBookmarks = savedEditor.ExtractBookmarks();
            savedBookmarkCount = savedBookmarks.Count;
        }

        // Validate the bookmark count
        if (savedBookmarkCount == originalBookmarkCount)
        {
            Console.WriteLine($"Bookmark validation succeeded. Count: {savedBookmarkCount}");
        }
        else
        {
            Console.WriteLine($"Bookmark validation failed. Expected: {originalBookmarkCount}, Found: {savedBookmarkCount}");
        }
    }
}