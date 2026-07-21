using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputPdf  = "output_with_bookmarks.pdf"; // file after processing
        const int    expectedCount = 5;                 // expected number of bookmarks

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // -----------------------------------------------------------------
            // Bind the source PDF, optionally modify bookmarks, then save it.
            // -----------------------------------------------------------------
            using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
            {
                editor.BindPdf(inputPdf);

                // Example: create bookmarks for all pages (remove if not needed)
                // editor.CreateBookmarks();

                // Save the PDF after any bookmark operations.
                editor.Save(outputPdf);
            }

            // -----------------------------------------------------------------
            // Verify the number of bookmarks in the saved PDF.
            // -----------------------------------------------------------------
            using (PdfBookmarkEditor verifier = new PdfBookmarkEditor())
            {
                verifier.BindPdf(outputPdf);

                // Extract all bookmarks (recursive).
                Bookmarks bookmarks = verifier.ExtractBookmarks();

                int actualCount = bookmarks?.Count ?? 0;
                Console.WriteLine($"Bookmarks found: {actualCount}");

                if (actualCount == expectedCount)
                {
                    Console.WriteLine("Bookmark count matches the expected value.");
                }
                else
                {
                    Console.WriteLine($"Bookmark count mismatch. Expected: {expectedCount}, Actual: {actualCount}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}