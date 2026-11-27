using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xmlPath = "bookmarks_list.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // PdfBookmarkEditor works as an instance. Bind the PDF first, then export the bookmarks.
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(pdfPath);               // Load the source PDF
            editor.ExportBookmarksToXML(xmlPath);   // Export all bookmarks to the XML file
        }

        Console.WriteLine($"Bookmarks exported to '{xmlPath}'.");
    }
}
