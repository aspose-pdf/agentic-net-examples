using System;
using System.Collections.Generic;
using Aspose.Pdf.Facades;   // PdfBookmarkEditor
using Aspose.Pdf;          // Document

// Example record representing a bookmark retrieved from a database
public class BookmarkRecord
{
    public string Title { get; set; }
    public int    PageNumber { get; set; }

    // Convenience constructor to satisfy non‑nullable warnings
    public BookmarkRecord(string title, int pageNumber)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
        PageNumber = pageNumber;
    }
}

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "input.pdf";          // PDF to which bookmarks will be added
        const string outputPdfPath = "output_bookmarked.pdf";

        // -----------------------------------------------------------------
        // STEP 0: Create a minimal PDF file so the example can run in a sandbox
        // -----------------------------------------------------------------
        CreateSeedPdf(sourcePdfPath);

        // -----------------------------------------------------------------
        // STEP 1: Retrieve bookmark data from a database.
        // In a real scenario replace this stub with actual DB access code.
        // -----------------------------------------------------------------
        List<BookmarkRecord> dbBookmarks = GetBookmarksFromDatabase();

        // -----------------------------------------------------------------
        // STEP 2: Open the PDF via PdfBookmarkEditor and import the bookmarks.
        // -----------------------------------------------------------------
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Bind the existing PDF file
            editor.BindPdf(sourcePdfPath);

            // Add each bookmark from the database
            foreach (BookmarkRecord rec in dbBookmarks)
            {
                // Create a flat bookmark that points to the specified page
                // CreateBookmarkOfPage(string bookmarkName, int pageNumber)
                editor.CreateBookmarkOfPage(rec.Title, rec.PageNumber);
            }

            // Save the modified PDF with the new bookmarks
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"Bookmarks imported and saved to '{outputPdfPath}'.");
    }

    // -----------------------------------------------------------------
    // Helper: generate a simple one‑page PDF so the demo has an input file.
    // -----------------------------------------------------------------
    private static void CreateSeedPdf(string path)
    {
        using (Document seed = new Document())
        {
            // Add a single blank page (Aspose.Pdf adds a default page when none exist)
            seed.Pages.Add();
            seed.Save(path);
        }
    }

    // -----------------------------------------------------------------
    // Mock method simulating a database query.
    // Replace with real ADO.NET / Entity Framework code as needed.
    // -----------------------------------------------------------------
    private static List<BookmarkRecord> GetBookmarksFromDatabase()
    {
        // Example static data; in practice this would come from a DB query.
        return new List<BookmarkRecord>
        {
            new BookmarkRecord("Chapter 1", 1),
            new BookmarkRecord("Chapter 2", 5),
            new BookmarkRecord("Appendix", 12)
        };
    }
}
