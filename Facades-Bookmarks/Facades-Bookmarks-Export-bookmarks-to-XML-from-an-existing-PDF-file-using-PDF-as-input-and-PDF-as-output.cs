using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source PDF file
        const string inputPdfPath = "input.pdf";

        // Path where the exported bookmarks XML will be saved
        const string outputXmlPath = "bookmarks.xml";

        // Verify that the input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        // Create the PdfBookmarkEditor facade and bind the PDF document
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            // Load the PDF file into the facade
            bookmarkEditor.BindPdf(inputPdfPath);

            // Export all bookmarks to the specified XML file
            bookmarkEditor.ExportBookmarksToXML(outputXmlPath);
        }

        Console.WriteLine($"Bookmarks successfully exported to '{outputXmlPath}'.");
    }
}