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

        // PdfBookmarkEditor handles bookmark export; it does not implement IDisposable.
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

        // Bind the PDF document before performing any bookmark operation.
        bookmarkEditor.BindPdf(pdfPath);

        // Export the complete bookmark (outline) hierarchy to an XML file.
        bookmarkEditor.ExportBookmarksToXML(xmlPath);

        Console.WriteLine($"Bookmarks exported to '{xmlPath}'.");
    }
}