using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string importXml = "extra.xml";
        const string exportXml = "full_export.xml";

        // Verify required files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(importXml))
        {
            Console.Error.WriteLine($"Import XML not found: {importXml}");
            return;
        }

        try
        {
            // PdfBookmarkEditor does NOT implement IDisposable, so no using block
            PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

            // Bind the PDF document to the editor
            bookmarkEditor.BindPdf(pdfPath);

            // Import bookmarks from the external XML file (correct method name)
            bookmarkEditor.ImportBookmarksWithXML(importXml);

            // Export the complete bookmark list to a new XML file (correct method name)
            bookmarkEditor.ExportBookmarksToXML(exportXml);

            Console.WriteLine($"Bookmarks successfully exported to '{exportXml}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
