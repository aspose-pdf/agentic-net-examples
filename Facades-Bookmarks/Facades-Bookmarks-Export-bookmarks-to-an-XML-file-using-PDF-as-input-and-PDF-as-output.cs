using System;
using System.IO;
using Aspose.Pdf.Facades;

class ExportBookmarks
{
    static void Main(string[] args)
    {
        // Paths for the source PDF, the output PDF (unchanged), and the exported bookmarks XML.
        string inputPdfPath = "input.pdf";
        string outputPdfPath = "output.pdf";
        string bookmarksXmlPath = "bookmarks.xml";

        // Verify that the input PDF file exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF not found at '{inputPdfPath}'.");
            return;
        }

        // Use PdfBookmarkEditor to work with bookmarks.
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            // Load the PDF document into the editor.
            bookmarkEditor.BindPdf(inputPdfPath);

            // Export all bookmarks to an XML file.
            bookmarkEditor.ExportBookmarksToXML(bookmarksXmlPath);

            // Save the (unchanged) PDF to the desired output location.
            bookmarkEditor.Save(outputPdfPath);
        }

        Console.WriteLine($"Bookmarks successfully exported to '{bookmarksXmlPath}'.");
        Console.WriteLine($"PDF saved to '{outputPdfPath}'.");
    }
}