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
        const int expectedBookmarks = 3; // set the expected number of bookmarks

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF, then save it to a new file
        using (Document doc = new Document(inputPath))
        {
            doc.Save(outputPath);
        }

        // Extract bookmarks from the saved PDF using the correct facade class
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            bookmarkEditor.BindPdf(outputPath);
            Bookmarks bookmarks = bookmarkEditor.ExtractBookmarks();
            int actualCount = bookmarks?.Count ?? 0;

            // Validate the bookmark count
            if (actualCount == expectedBookmarks)
            {
                Console.WriteLine($"Validation succeeded: {actualCount} bookmarks found as expected.");
            }
            else
            {
                Console.WriteLine($"Validation failed: expected {expectedBookmarks} bookmarks, but found {actualCount}.");
            }
        }
    }
}
