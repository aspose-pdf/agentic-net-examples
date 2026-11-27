using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string bookmarksXml = "fresh.xml";
        const string outputPdfPath = "output.pdf";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(bookmarksXml))
        {
            Console.Error.WriteLine($"Bookmarks XML not found: {bookmarksXml}");
            return;
        }

        // PdfBookmarkEditor implements IDisposable, so wrap it in a using block
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPdfPath);

            // Remove all existing bookmarks (no‑argument overload deletes everything)
            editor.DeleteBookmarks();

            // Import fresh bookmarks from the XML file (method name includes "WithXML")
            editor.ImportBookmarksWithXML(bookmarksXml);

            // Save the modified PDF to a new file
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"Bookmarks updated and saved to '{outputPdfPath}'.");
    }
}
