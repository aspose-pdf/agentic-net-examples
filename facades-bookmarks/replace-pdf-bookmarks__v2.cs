using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";      // source PDF
        const string outputPath = "output.pdf"; // PDF after bookmark update
        const string bookmarksXml = "new_set.xml"; // XML file with new bookmarks

        // Verify required files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(bookmarksXml))
        {
            Console.Error.WriteLine($"Bookmarks XML not found: {bookmarksXml}");
            return;
        }

        // PdfBookmarkEditor implements IDisposable, so we can use a using block.
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Bind the PDF document to the editor
            editor.BindPdf(pdfPath);

            // Remove all existing bookmarks (no‑argument overload deletes everything)
            editor.DeleteBookmarks();

            // Import the new set of bookmarks from the XML file (method name includes "WithXML")
            editor.ImportBookmarksWithXML(bookmarksXml);

            // Save the modified PDF to a new file
            editor.Save(outputPath);
        }

        Console.WriteLine($"Bookmarks updated and saved to '{outputPath}'.");
    }
}
