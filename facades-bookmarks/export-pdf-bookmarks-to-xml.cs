using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF containing the bookmarks
        const string inputPdfPath = "input.pdf";

        // Output XML file that will hold the exported bookmarks
        const string outputXmlPath = "bookmarks.xml";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Initialize the bookmark editor facade
            PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

            // Bind the PDF document to the editor
            bookmarkEditor.BindPdf(inputPdfPath);

            // Export the complete bookmark hierarchy to an XML file
            bookmarkEditor.ExportBookmarksToXML(outputXmlPath);

            // Release resources held by the facade
            bookmarkEditor.Close();

            Console.WriteLine($"Bookmarks successfully exported to '{outputXmlPath}'.");
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}