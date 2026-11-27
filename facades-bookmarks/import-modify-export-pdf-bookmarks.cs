using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string importXml = "hierarchy.xml";
        const string exportXml = "final.xml";
        const string bookmarkToDelete = "Draft";

        // Verify required files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(importXml))
        {
            Console.Error.WriteLine($"Bookmark XML not found: {importXml}");
            return;
        }

        try
        {
            // PdfBookmarkEditor does NOT implement IDisposable, so we instantiate directly
            var bookmarkEditor = new PdfBookmarkEditor();

            // Bind the PDF document to the editor
            bookmarkEditor.BindPdf(pdfPath);

            // Import bookmark hierarchy from the XML file (correct method name)
            bookmarkEditor.ImportBookmarksWithXML(importXml);

            // Delete the bookmark with the specified title (overload that accepts a title)
            bookmarkEditor.DeleteBookmarks(bookmarkToDelete);

            // Export the resulting bookmark list to a new XML file (correct method name)
            bookmarkEditor.ExportBookmarksToXML(exportXml);

            // Release resources held by the editor
            bookmarkEditor.Close();

            Console.WriteLine($"Bookmarks processed successfully. Output saved to '{exportXml}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
