using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Path for the exported bookmarks XML
        const string bookmarksXmlPath = "bookmarks.xml";

        // Output PDF file path (the same PDF after processing)
        const string outputPdfPath = "output.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Initialize the bookmark editor facade
            PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

            // Bind the PDF file to the editor
            bookmarkEditor.BindPdf(inputPdfPath);

            // Export all bookmarks to an XML file
            bookmarkEditor.ExportBookmarksToXML(bookmarksXmlPath);
            Console.WriteLine($"Bookmarks exported to XML: {bookmarksXmlPath}");

            // Save the (unchanged) PDF to the output path
            bookmarkEditor.Save(outputPdfPath);
            Console.WriteLine($"PDF saved to: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}