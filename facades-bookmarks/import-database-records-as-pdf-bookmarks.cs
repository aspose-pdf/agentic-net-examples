using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string outputPdf = "output_with_bookmarks.pdf";
        const int evaluationLimit = 4; // Aspose.PDF evaluation mode allows max 4 items per collection

        // Simulated result set from a database query:
        // Each tuple contains the bookmark title and the target page number.
        var dbRecords = new List<(string Title, int Page)>
        {
            ("Introduction", 1),
            ("Chapter 1", 3),
            ("Chapter 2", 7),
            ("Conclusion", 12),
            // Any additional records would exceed the evaluation limit and cause an exception.
        };

        // Limit the records to the evaluation‑mode maximum.
        var limitedRecords = dbRecords.Take(evaluationLimit).ToList();

        // Determine the highest page number required for the dummy document, respecting the limit.
        int maxPage = limitedRecords.Max(r => r.Page);
        if (maxPage > evaluationLimit)
            maxPage = evaluationLimit; // cap page count to 4 to stay within evaluation limits

        // ---------------------------------------------------------------------
        // Create an in‑memory PDF with enough pages so that bookmarks have a target.
        // This removes the dependency on an external "input.pdf" file and fixes the
        // FileNotFoundException observed at runtime.
        // ---------------------------------------------------------------------
        using (Document doc = new Document())
        {
            // Add blank pages up to the required (capped) maximum.
            for (int i = 0; i < maxPage; i++)
                doc.Pages.Add();

            // Initialise the bookmark editor and bind the generated document.
            PdfBookmarkEditor editor = new PdfBookmarkEditor();
            editor.BindPdf(doc);

            // Convert each (limited) database record into a PdfBookmark and insert it.
            foreach (var rec in limitedRecords)
            {
                // Create a bookmark instance.
                Bookmark bm = new Bookmark
                {
                    Title = rec.Title,
                    PageNumber = rec.Page
                };

                // Add the bookmark to the document.
                editor.CreateBookmarks(bm);
            }

            // Save the modified PDF (save rule) and release resources.
            editor.Save(outputPdf);
            editor.Close();
        }

        Console.WriteLine($"Bookmarks imported and saved to '{outputPdf}'.");
    }
}
