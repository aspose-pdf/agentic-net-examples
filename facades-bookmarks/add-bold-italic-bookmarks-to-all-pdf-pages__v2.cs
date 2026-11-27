using System;
using System.IO;
using Aspose.Pdf;                     // Bookmark class
using Aspose.Pdf.Facades;            // PdfBookmarkEditor

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "bookmarked_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the document to obtain the page count (PdfBookmarkEditor has no GetPageCount)
        Document pdfDoc = new Document(inputPath);
        int pageCount = pdfDoc.Pages.Count;

        // Create the bookmark editor and bind the source PDF
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            bookmarkEditor.BindPdf(inputPath);

            // Create a bookmark for each page with both bold and italic styles
            for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
            {
                Bookmark bm = new Bookmark
                {
                    Title      = $"Page {pageNumber}",
                    PageNumber = pageNumber,
                    BoldFlag   = true,
                    ItalicFlag = true
                };

                // Add the bookmark to the document
                bookmarkEditor.CreateBookmarks(bm);
            }

            // Save the PDF with the newly added bookmarks
            bookmarkEditor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
    }
}
