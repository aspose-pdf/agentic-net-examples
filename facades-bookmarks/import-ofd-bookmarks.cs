using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string ofdPath = "input.ofd";
        const string outputPdf = "output.pdf";

        if (!File.Exists(ofdPath))
        {
            Console.Error.WriteLine($"File not found: {ofdPath}");
            return;
        }

        // Load the OFD file as a PDF document
        using (Document pdfDoc = new Document(ofdPath, new OfdLoadOptions()))
        {
            // Initialize the bookmark editor with the loaded document
            PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor(pdfDoc);

            // Create a simple bookmark for each page
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                string bookmarkTitle = $"Page {pageIndex}";
                bookmarkEditor.CreateBookmarkOfPage(bookmarkTitle, pageIndex);
            }

            // Save the PDF with the imported bookmarks
            bookmarkEditor.Save(outputPdf);
            bookmarkEditor.Close();
        }

        Console.WriteLine($"Bookmarks imported and PDF saved to '{outputPdf}'.");
    }
}