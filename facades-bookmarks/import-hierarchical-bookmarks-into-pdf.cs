using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf      = "input.pdf";
        const string bookmarksXml  = "hierarchy.xml";
        const string outputPdf     = "output.pdf";

        // Verify that the source files exist.
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(bookmarksXml))
        {
            Console.Error.WriteLine($"Bookmarks XML not found: {bookmarksXml}");
            return;
        }

        // PdfBookmarkEditor does not implement IDisposable, so a plain instance is sufficient.
        PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

        // Bind the target PDF document.
        bookmarkEditor.BindPdf(inputPdf);

        // Import hierarchical bookmarks from the XML file.
        // Correct method name is ImportBookmarksWithXML (not ImportBookmarks).
        bookmarkEditor.ImportBookmarksWithXML(bookmarksXml);

        // Save the modified PDF with the imported bookmarks.
        bookmarkEditor.Save(outputPdf);

        Console.WriteLine($"Bookmarks imported and saved to '{outputPdf}'.");
    }
}
