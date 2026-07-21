using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";
        const string outputPdfPath = "bookmarked_output.pdf";

        // ---------------------------------------------------------------------
        // Ensure the source PDF exists. In sandbox examples we must create the
        // file inline because the environment starts empty.
        // ---------------------------------------------------------------------
        if (!System.IO.File.Exists(sourcePdfPath))
        {
            using (Document doc = new Document())
            {
                doc.Pages.Add(); // add a single blank page
                doc.Save(sourcePdfPath);
            }
        }

        // ---------------------------------------------------------------------
        // In‑memory DataTable that mimics a database result set.
        // ---------------------------------------------------------------------
        DataTable bookmarksTable = new DataTable();
        bookmarksTable.Columns.Add("Title", typeof(string));
        bookmarksTable.Columns.Add("PageNumber", typeof(int));

        bookmarksTable.Rows.Add("Chapter 1", 1);
        bookmarksTable.Rows.Add("Chapter 2", 5);
        bookmarksTable.Rows.Add("Conclusion", 12);

        // ---------------------------------------------------------------------
        // Add bookmarks to the PDF.
        // ---------------------------------------------------------------------
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(sourcePdfPath);

            foreach (DataRow row in bookmarksTable.Rows)
            {
                var bm = new Aspose.Pdf.Facades.Bookmark
                {
                    Title = (string)row["Title"],
                    PageNumber = (int)row["PageNumber"],
                    Action = "GoTo" // standard navigation action
                };

                editor.CreateBookmarks(bm);
            }

            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"Bookmarks imported and saved to '{outputPdfPath}'.");
    }
}
