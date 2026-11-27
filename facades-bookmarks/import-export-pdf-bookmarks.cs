using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string importBookmarks = "new_bookmarks.xml";
        const string exportBookmarks = "combined.xml";

        // Verify required files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(importBookmarks))
        {
            Console.Error.WriteLine($"Bookmark XML not found: {importBookmarks}");
            return;
        }

        try
        {
            // PdfBookmarkEditor does NOT implement IDisposable; no using block needed
            PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

            // Bind the PDF document to the editor
            bookmarkEditor.BindPdf(pdfPath);

            // Import bookmarks from the external XML file (correct method name)
            bookmarkEditor.ImportBookmarksWithXML(importBookmarks);

            // Export the combined bookmark list (original + imported) to a new XML file (correct method name)
            bookmarkEditor.ExportBookmarksToXML(exportBookmarks);

            Console.WriteLine($"Bookmarks imported from '{importBookmarks}' and exported to '{exportBookmarks}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing bookmarks: {ex.Message}");
        }
    }
}
