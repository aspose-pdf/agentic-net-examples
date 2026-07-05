using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added for TextFragment

class Program
{
    static void Main()
    {
        // Paths for the intermediate and final PDF files
        const string tempPdfPath = "temp.pdf";
        const string bookmarkedPdfPath = "bookmarked.pdf";

        // -----------------------------------------------------------------
        // Step 1: Create a simple PDF with a few pages
        // -----------------------------------------------------------------
        using (Document doc = new Document())
        {
            // Add three pages with simple text
            for (int i = 1; i <= 3; i++)
            {
                Page page = doc.Pages.Add();
                // Fully qualified Rectangle to avoid ambiguity
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 500, 750);
                TextFragment tf = new TextFragment($"Page {i}");
                page.Paragraphs.Add(tf);
            }

            // Save the temporary PDF (no SaveOptions needed for PDF)
            doc.Save(tempPdfPath);
        }

        // -----------------------------------------------------------------
        // Step 2: Create bookmarks for all pages and save the new PDF
        // -----------------------------------------------------------------
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Bind the editor to the temporary PDF
            editor.BindPdf(tempPdfPath);

            // Create a bookmark for each page (default titles)
            editor.CreateBookmarks();

            // Save the PDF with bookmarks
            editor.Save(bookmarkedPdfPath);
        }

        // -----------------------------------------------------------------
        // Step 3: Extract bookmarks from the saved PDF and validate count
        // -----------------------------------------------------------------
        using (PdfBookmarkEditor validator = new PdfBookmarkEditor())
        {
            validator.BindPdf(bookmarkedPdfPath);

            // Extract all bookmarks (recursive)
            Bookmarks bookmarks = validator.ExtractBookmarks();

            // Expected number of bookmarks equals the number of pages (3)
            int expectedCount = 3;
            int actualCount = bookmarks.Count;

            Console.WriteLine($"Expected bookmarks: {expectedCount}");
            Console.WriteLine($"Actual bookmarks  : {actualCount}");

            if (actualCount == expectedCount)
                Console.WriteLine("Bookmark validation succeeded.");
            else
                Console.WriteLine("Bookmark validation failed.");
        }

        // Cleanup temporary files (optional)
        try { File.Delete(tempPdfPath); } catch { }
    }
}
