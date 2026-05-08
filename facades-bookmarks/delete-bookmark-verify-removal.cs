using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF with bookmarks
        const string outputPdf = "output.pdf";         // PDF after deletion
        const string bookmarkTitle = "Bookmark To Delete"; // exact title of the bookmark to remove

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Delete the specified bookmark and save the result
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPdf);
            editor.DeleteBookmarks(bookmarkTitle);   // remove bookmark by title
            editor.Save(outputPdf);
        }

        // Verify that the bookmark has been removed by extracting remaining bookmarks
        using (PdfBookmarkEditor verifier = new PdfBookmarkEditor())
        {
            verifier.BindPdf(outputPdf);
            var remaining = verifier.ExtractBookmarks(); // get all bookmarks

            Console.WriteLine("Remaining bookmarks after deletion:");
            foreach (Bookmark bm in remaining)
            {
                Console.WriteLine($"- Title: {bm.Title}, Page: {bm.PageNumber}");
            }
        }
    }
}