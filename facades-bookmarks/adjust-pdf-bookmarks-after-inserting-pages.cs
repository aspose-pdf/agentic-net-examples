using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string originalPdf = "input.pdf";
        const string outputPdf   = "output.pdf";
        const int pagesToInsert = 2; // number of blank pages to prepend

        if (!File.Exists(originalPdf))
        {
            Console.Error.WriteLine($"Source file not found: {originalPdf}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Create a temporary PDF that contains the blank pages to insert.
        // -----------------------------------------------------------------
        string blankPdf = Path.GetTempFileName();
        using (Document blankDoc = new Document())
        {
            for (int i = 0; i < pagesToInsert; i++)
                blankDoc.Pages.Add(); // adds a blank A4 page by default

            blankDoc.Save(blankPdf);
        }

        // -----------------------------------------------------------------
        // 2. Load the original document and prepend the blank pages.
        //    Document.Pages.Insert is used because PdfFileEditor has no InsertPage.
        // -----------------------------------------------------------------
        using (Document originalDoc = new Document(originalPdf))
        using (Document blankDoc = new Document(blankPdf))
        {
            // Insert pages in reverse order so that the final order matches the source.
            for (int i = pagesToInsert; i >= 1; i--)
            {
                // Clone the page from the blank document to avoid moving it.
                Page pageToInsert = blankDoc.Pages[i];
                originalDoc.Pages.Insert(1, pageToInsert);
            }

            originalDoc.Save(outputPdf);
        }

        // Clean up the temporary blank PDF.
        if (File.Exists(blankPdf))
            File.Delete(blankPdf);

        // -----------------------------------------------------------------
        // 3. Adjust bookmark destinations to account for the inserted pages.
        // -----------------------------------------------------------------
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();
        bookmarkEditor.BindPdf(outputPdf);
        // ExtractBookmarks returns a Bookmarks collection (IEnumerable<Bookmark>)
        Bookmarks rootBookmarks = bookmarkEditor.ExtractBookmarks();

        // Recursively shift each bookmark's page number.
        void ShiftBookmark(Bookmark bm, int offset)
        {
            if (bm.PageNumber > 0)
                bm.PageNumber += offset;

            if (bm.ChildItems != null)
            {
                foreach (Bookmark child in bm.ChildItems)
                    ShiftBookmark(child, offset);
            }
        }

        foreach (Bookmark bm in rootBookmarks)
            ShiftBookmark(bm, pagesToInsert);

        // Save the updated bookmarks back into the PDF.
        bookmarkEditor.Save(outputPdf);

        Console.WriteLine($"Bookmarks updated and pages inserted. Result saved to '{outputPdf}'.");
    }
}
