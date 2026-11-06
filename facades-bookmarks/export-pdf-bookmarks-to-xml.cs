using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xmlPath = "bookmarks.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // PdfBookmarkEditor does not implement IDisposable, so we instantiate it directly.
            PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

            // Bind the PDF document to the editor.
            bookmarkEditor.BindPdf(pdfPath);

            // Export the complete bookmark hierarchy to an XML file.
            // Correct method name is ExportBookmarksToXML (instance method).
            bookmarkEditor.ExportBookmarksToXML(xmlPath);

            Console.WriteLine($"Bookmarks exported to '{xmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error exporting bookmarks: {ex.Message}");
        }
    }
}
