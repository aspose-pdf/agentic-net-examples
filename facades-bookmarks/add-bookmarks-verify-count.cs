using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // source PDF
        const string outputPath = "bookmarked.pdf";    // PDF after adding bookmarks
        const int expectedBookmarks = 3;               // number of bookmarks we expect

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the document, add bookmarks, and save it
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has enough pages for the desired bookmarks
            if (doc.Pages.Count < expectedBookmarks)
            {
                Console.Error.WriteLine("Not enough pages to create the required bookmarks.");
                return;
            }

            // PdfBookmarkEditor works on the loaded Document instance
            using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
            {
                editor.BindPdf(doc);

                // Create a bookmark for each of the first 'expectedBookmarks' pages
                for (int i = 1; i <= expectedBookmarks; i++)
                {
                    editor.CreateBookmarkOfPage($"Page {i} bookmark", i);
                }

                // Save the modified PDF
                editor.Save(outputPath);
            }
        }

        // Re-open the saved PDF and extract its bookmarks to validate the count
        using (PdfBookmarkEditor validator = new PdfBookmarkEditor())
        {
            validator.BindPdf(outputPath);
            Bookmarks bookmarks = validator.ExtractBookmarks(); // extracts all levels
            int actualCount = bookmarks.Count;

            Console.WriteLine($"Bookmarks found: {actualCount}");
            if (actualCount == expectedBookmarks)
                Console.WriteLine("Bookmark count validation passed.");
            else
                Console.WriteLine($"Bookmark count validation failed. Expected {expectedBookmarks}, but found {actualCount}.");
        }
    }
}