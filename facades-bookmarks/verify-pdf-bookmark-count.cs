using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the intermediate file with bookmarks, and the final saved PDF
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output_with_bookmarks.pdf";

        // Expected number of bookmarks after the operation
        const int expectedBookmarkCount = 5; // adjust as needed

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // ------------------------------------------------------------
        // Step 1: Load the source PDF and create bookmarks for all pages
        // ------------------------------------------------------------
        using (Document srcDoc = new Document(inputPdfPath))
        {
            // PdfBookmarkEditor works on a PDF document via binding
            PdfBookmarkEditor editor = new PdfBookmarkEditor();
            editor.BindPdf(srcDoc);

            // Create a bookmark for each page in the document
            editor.CreateBookmarks();

            // Save the document with the newly created bookmarks
            editor.Save(outputPdfPath);

            // Release resources held by the editor
            editor.Close();
        }

        // ------------------------------------------------------------
        // Step 2: Re-open the saved PDF and verify the bookmark count
        // ------------------------------------------------------------
        using (Document savedDoc = new Document(outputPdfPath))
        {
            PdfBookmarkEditor verifyEditor = new PdfBookmarkEditor();
            verifyEditor.BindPdf(savedDoc);

            // Extract all bookmarks from the document
            Bookmarks bookmarks = verifyEditor.ExtractBookmarks();

            int actualCount = bookmarks.Count;

            // Output the verification result
            if (actualCount == expectedBookmarkCount)
            {
                Console.WriteLine($"Success: Bookmark count matches expected value ({actualCount}).");
            }
            else
            {
                Console.WriteLine($"Failure: Expected {expectedBookmarkCount} bookmarks, but found {actualCount}.");
            }

            // Clean up
            verifyEditor.Close();
        }
    }
}