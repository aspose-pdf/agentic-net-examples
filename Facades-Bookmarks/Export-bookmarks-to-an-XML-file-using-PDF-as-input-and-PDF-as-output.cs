using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class ExportBookmarks
{
    static void Main()
    {
        // Input PDF file (must exist)
        const string inputPdfPath  = "input.pdf";
        // Output PDF file (can be the same as input or a copy)
        const string outputPdfPath = "output.pdf";
        // XML file that will contain the exported bookmarks
        const string bookmarksXmlPath = "bookmarks.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Use PdfBookmarkEditor facade to work with bookmarks
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Bind the PDF document to the editor
            editor.BindPdf(inputPdfPath);

            // Export all bookmarks to an XML file
            editor.ExportBookmarksToXML(bookmarksXmlPath);

            // Save the (unchanged) PDF to the output path
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"Bookmarks exported to '{bookmarksXmlPath}'.");
        Console.WriteLine($"PDF saved to '{outputPdfPath}'.");
    }
}