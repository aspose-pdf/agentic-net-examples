using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfPath      = "input.pdf";
        const string bookmarksXml = "source.xml";
        const string outputPdf    = "output.pdf";

        // Verify input files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(bookmarksXml))
        {
            Console.Error.WriteLine($"Bookmarks XML not found: {bookmarksXml}");
            return;
        }

        // PdfBookmarkEditor does not implement IDisposable, so no using block is required
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

        // Bind the existing PDF document
        bookmarkEditor.BindPdf(pdfPath);

        // Import bookmarks from the XML file – correct method name
        bookmarkEditor.ImportBookmarksWithXML(bookmarksXml);

        // Save the updated PDF with the new bookmark structure
        bookmarkEditor.Save(outputPdf);

        Console.WriteLine($"Bookmarks imported and PDF saved to '{outputPdf}'.");
    }
}
