using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfPath      = "input.pdf";      // Existing PDF file
        const string bookmarksXml = "bookmarks.xml"; // XML file containing bookmarks
        const string outputPdf    = "output.pdf";     // PDF with imported bookmarks

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

        // PdfBookmarkEditor does NOT implement IDisposable, so no using block is required
        PdfBookmarkEditor editor = new PdfBookmarkEditor();

        // Bind the existing PDF document
        editor.BindPdf(pdfPath);

        // Import bookmarks from the XML file
        editor.ImportBookmarksWithXML(bookmarksXml);

        // Save the modified PDF to a new file
        editor.Save(outputPdf);

        Console.WriteLine($"Bookmarks imported successfully. Output saved to '{outputPdf}'.");
    }
}