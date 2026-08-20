using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";          // source PDF
        const string outputPath     = "output_with_bm.pdf"; // PDF after adding bookmarks
        const int    expectedCount  = 5;                    // expected number of bookmarks

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF and add bookmarks for all pages
        using (Document doc = new Document(inputPath))
        {
            // PdfBookmarkEditor works on the Document instance
            using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
            {
                editor.BindPdf(doc);          // initialize the facade with the document
                editor.CreateBookmarks();     // create a bookmark for each page
                editor.Save(outputPath);      // persist changes to a new file
            }
        }

        // Re-open the saved PDF and extract its bookmarks
        using (PdfBookmarkEditor extractor = new PdfBookmarkEditor())
        {
            extractor.BindPdf(outputPath);               // load the saved PDF
            Bookmarks bookmarks = extractor.ExtractBookmarks(); // get all bookmarks
            int actualCount = bookmarks.Count;

            Console.WriteLine($"Expected bookmarks: {expectedCount}");
            Console.WriteLine($"Actual bookmarks  : {actualCount}");

            if (actualCount == expectedCount)
                Console.WriteLine("Bookmark count validation succeeded.");
            else
                Console.WriteLine("Bookmark count validation failed.");
        }
    }
}