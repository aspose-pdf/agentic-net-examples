using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string inputPdfPath = "input.pdf";
        const string bookmarksXmlPath = "bookmarks.xml";
        const string outputPdfPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // -----------------------------------------------------------------
            // Step 1: Export bookmarks from the source PDF to an XML file
            // -----------------------------------------------------------------
            using (PdfBookmarkEditor exporter = new PdfBookmarkEditor())
            {
                // Bind the source PDF to the editor
                exporter.BindPdf(inputPdfPath);

                // Export all bookmarks to the specified XML file
                exporter.ExportBookmarksToXML(bookmarksXmlPath);
            }

            // -----------------------------------------------------------------
            // Step 2: Import the previously exported bookmarks into a new PDF
            // -----------------------------------------------------------------
            // For demonstration we start from the same source PDF.
            // In a real scenario you could load a different PDF here.
            using (PdfBookmarkEditor importer = new PdfBookmarkEditor())
            {
                // Bind the PDF that will receive the bookmarks
                importer.BindPdf(inputPdfPath);

                // Import bookmarks from the XML file
                importer.ImportBookmarksWithXML(bookmarksXmlPath);

                // Save the resulting PDF with the imported bookmarks
                importer.Save(outputPdfPath);
            }

            Console.WriteLine($"Bookmarks successfully exported to '{bookmarksXmlPath}' and imported into '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}