using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath    = "input.pdf";
        const string outputPath = "output_bookmarked.pdf";

        // ------------------------------------------------------------
        // 0️⃣ Ensure an input PDF exists – the sandbox starts empty.
        // ------------------------------------------------------------
        if (!File.Exists(pdfPath))
        {
            // Create a minimal one‑page PDF that will later receive bookmarks.
            using var placeholder = new Document();
            placeholder.Pages.Add();
            placeholder.Save(pdfPath);
        }

        // ------------------------------------------------------------
        // 1️⃣ Simulate the database result with an in‑memory DataTable
        // ------------------------------------------------------------
        DataTable bookmarksTable = new DataTable();
        bookmarksTable.Columns.Add("Title", typeof(string));
        bookmarksTable.Columns.Add("PageNumber", typeof(int));
        // Sample rows – replace with real data when integrating with a DB
        bookmarksTable.Rows.Add("Chapter 1", 1);
        bookmarksTable.Rows.Add("Chapter 2", 5);
        bookmarksTable.Rows.Add("Appendix", 12);

        // ------------------------------------------------------------
        // 2️⃣ Use the PdfBookmarkEditor (facade model) to create bookmarks
        // ------------------------------------------------------------
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Bind the existing PDF file.
            editor.BindPdf(pdfPath);

            foreach (DataRow row in bookmarksTable.Rows)
            {
                string title = (string)row["Title"];
                int    page  = (int)row["PageNumber"]; // 1‑based page index

                // The facade Bookmark type – not PdfBookmark!
                Bookmark bm = new Bookmark
                {
                    Title      = title,
                    PageNumber = page,
                    Level      = 1,               // top‑level entry
                    Action     = "GoTo"          // optional, defaults to GoTo
                };

                // Add a single bookmark to the document outline.
                editor.CreateBookmarks(bm);
            }

            // Save the PDF with the newly added bookmarks.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks imported and saved to '{outputPath}'.");
    }
}
