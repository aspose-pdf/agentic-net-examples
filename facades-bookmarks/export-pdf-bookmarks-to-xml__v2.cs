using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // source PDF (must exist)
        const string exportXmlPath = "export.xml";   // destination for bookmarks

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // PdfBookmarkEditor does NOT implement IDisposable; do NOT wrap in using.
            PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

            // Bind the existing PDF. This loads the document internally.
            bookmarkEditor.BindPdf(pdfPath);

            // Export the current bookmark tree to an XML file.
            // The method name is ExportBookmarksToXML (instance method).
            bookmarkEditor.ExportBookmarksToXML(exportXmlPath);

            Console.WriteLine($"Bookmarks exported successfully to '{exportXmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
