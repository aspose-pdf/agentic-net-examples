using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string ofdPath = "source.ofd";      // OFD file containing bookmarks
        const string pdfPath = "target.pdf";      // Existing PDF to receive bookmarks
        const string outputPath = "output.pdf";   // Resulting PDF with imported bookmarks

        if (!File.Exists(ofdPath))
        {
            Console.Error.WriteLine($"OFD file not found: {ofdPath}");
            return;
        }

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the OFD file as a PDF document (conversion is handled internally)
        using (Document ofdDoc = new Document(ofdPath, new OfdLoadOptions()))
        {
            // Load the target PDF where bookmarks will be added
            using (Document targetDoc = new Document(pdfPath))
            {
                // Initialize the bookmark editor on the target PDF
                PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();
                bookmarkEditor.BindPdf(targetDoc);

                // Example logic: create a bookmark for each page of the OFD document
                for (int i = 1; i <= ofdDoc.Pages.Count; i++)
                {
                    // Construct a Bookmark object (Aspose.Pdf.Facades.Bookmark)
                    Bookmark bm = new Bookmark
                    {
                        Title = $"OFD Page {i}",
                        PageNumber = i,
                        Action = "GoTo"
                    };

                    // Add the bookmark to the target PDF
                    bookmarkEditor.CreateBookmarks(bm);
                }

                // Save the updated PDF (standard PDF save)
                targetDoc.Save(outputPath);
            }
        }

        Console.WriteLine($"Bookmarks imported and saved to '{outputPath}'.");
    }
}