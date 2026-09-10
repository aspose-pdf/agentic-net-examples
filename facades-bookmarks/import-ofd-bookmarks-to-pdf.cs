using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string ofdPath   = "input.ofd";
        const string outputPdf = "output.pdf";

        if (!File.Exists(ofdPath))
        {
            Console.Error.WriteLine($"File not found: {ofdPath}");
            return;
        }

        // Load the OFD file (input format) and work with it as a PDF document
        using (Document doc = new Document(ofdPath, new OfdLoadOptions()))
        {
            // Initialize the bookmark editor on the loaded document
            PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor(doc);

            // Convert each page into a bookmark (PdfBookmark equivalent)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Bookmark bm = new Bookmark
                {
                    Title      = $"Page {i}",
                    PageNumber = i,
                    Action     = "GoTo"
                };
                // Add the bookmark to the document
                bookmarkEditor.CreateBookmarks(bm);
            }

            // Save the resulting PDF with the imported bookmarks
            bookmarkEditor.Save(outputPdf);
        }

        Console.WriteLine($"PDF with bookmarks saved to '{outputPdf}'.");
    }
}