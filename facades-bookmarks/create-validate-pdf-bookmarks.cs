using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const int    expectedBookmarks = 5; // adjust to the expected number

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF, create bookmarks for all pages, and save the result
        using (Document srcDoc = new Document(inputPath))
        {
            // Create bookmarks using the Facade editor
            PdfBookmarkEditor creator = new PdfBookmarkEditor();
            creator.BindPdf(srcDoc);
            creator.CreateBookmarks();               // creates a bookmark for each page
            creator.Save(outputPath);                // persist the changes
            creator.Close();                         // release facade resources
        }

        // Validate the number of bookmarks in the saved PDF
        PdfBookmarkEditor validator = new PdfBookmarkEditor();
        validator.BindPdf(outputPath);
        Aspose.Pdf.Facades.Bookmarks bookmarks = validator.ExtractBookmarks();
        int actualCount = bookmarks.Count;
        Console.WriteLine($"Bookmarks found: {actualCount}");

        if (actualCount == expectedBookmarks)
            Console.WriteLine("Bookmark count matches the expected value.");
        else
            Console.WriteLine($"Bookmark count mismatch. Expected {expectedBookmarks}, but found {actualCount}.");

        validator.Close(); // optional, releases resources
    }
}