using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputXmlPath = "remaining.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // PdfBookmarkEditor does NOT implement IDisposable, so no using block is required.
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

        // Bind the PDF document to the editor.
        bookmarkEditor.BindPdf(inputPdfPath);

        // Delete the bookmark named "References" if it exists.
        // Use the correct method name DeleteBookmarks (single‑argument overload deletes by title).
        bookmarkEditor.DeleteBookmarks("References");

        // Export the remaining bookmarks to an XML file.
        // The correct instance method is ExportBookmarksToXML.
        bookmarkEditor.ExportBookmarksToXML(outputXmlPath);

        // Optional: close the editor to release resources.
        bookmarkEditor.Close();

        Console.WriteLine($"Bookmarks exported to '{outputXmlPath}'.");
    }
}
