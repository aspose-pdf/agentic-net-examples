using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Output PDF file path
        const string outputPath = "output.pdf";

        // Create a new PDF document with a few pages
        Document pdfDoc = new Document();
        for (int i = 0; i < 3; i++)
        {
            // Add a new page
            Page page = pdfDoc.Pages.Add();

            // Add simple text to identify the page
            page.Paragraphs.Add(new TextFragment($"Page {i + 1}"));
        }

        // Use PdfBookmarkEditor to create bookmarks for each page
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            // Bind the editor to the document we just created
            bookmarkEditor.BindPdf(pdfDoc);

            // Create default bookmarks for all pages
            bookmarkEditor.CreateBookmarks();

            // Save the PDF (bookmarks are now part of the file)
            bookmarkEditor.Save(outputPath);
        }

        Console.WriteLine($"PDF with bookmarks saved to '{outputPath}'.");
    }
}