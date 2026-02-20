using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output file paths
        string inputPdfPath = "input.pdf";
        string outputPdfPath = "output.pdf";
        string bookmarksXmlPath = "bookmarks.xml";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF not found at '{inputPdfPath}'.");
            return;
        }

        // Initialize the PdfBookmarkEditor and bind the source PDF
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            // Load the PDF document
            bookmarkEditor.BindPdf(inputPdfPath);

            // Export current bookmarks to an XML file
            bookmarkEditor.ExportBookmarksToXML(bookmarksXmlPath);

            // (Optional) Modify the XML file here if needed

            // Import bookmarks from the XML back into the PDF
            bookmarkEditor.ImportBookmarksWithXML(bookmarksXmlPath);

            // Save the updated PDF to the output path
            bookmarkEditor.Save(outputPdfPath);
        }

        Console.WriteLine($"Bookmarks exported to '{bookmarksXmlPath}' and PDF saved as '{outputPdfPath}'.");
    }
}