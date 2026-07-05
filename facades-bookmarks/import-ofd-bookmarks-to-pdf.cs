using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string ofdPath = "source.ofd";          // input OFD file containing bookmarks
        const string outputPdfPath = "result.pdf";   // output PDF with imported bookmarks

        if (!File.Exists(ofdPath))
        {
            Console.Error.WriteLine($"Input file not found: {ofdPath}");
            return;
        }

        // Load the OFD file into a PdfDocument using OfdLoadOptions.
        Document ofdDocument = new Document(ofdPath, new OfdLoadOptions());

        // Use PdfBookmarkEditor to extract the bookmarks from the loaded document.
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            // Bind the already‑loaded PdfDocument (the overload that accepts a Document).
            bookmarkEditor.BindPdf(ofdDocument);

            // Extract the bookmarks that were read from the OFD file.
            Bookmarks extracted = bookmarkEditor.ExtractBookmarks();

            // If any bookmarks were found, add them to the PDF document.
            if (extracted != null && extracted.Count > 0)
            {
                // The CreateBookmarks method expects a single Bookmark (the root of a hierarchy).
                // Iterate through the collection and add each top‑level bookmark individually.
                foreach (Bookmark bm in extracted)
                {
                    bookmarkEditor.CreateBookmarks(bm);
                }
            }

            // Save the resulting PDF with the imported bookmarks.
            bookmarkEditor.Save(outputPdfPath);
        }

        Console.WriteLine($"Bookmarks imported and PDF saved to '{outputPdfPath}'.");
    }
}
