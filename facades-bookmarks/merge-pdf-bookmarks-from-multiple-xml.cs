using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string batch1Xml = "batch1.xml";
        const string batch2Xml = "batch2.xml";

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(batch1Xml))
        {
            Console.Error.WriteLine($"Bookmark file not found: {batch1Xml}");
            return;
        }
        if (!File.Exists(batch2Xml))
        {
            Console.Error.WriteLine($"Bookmark file not found: {batch2Xml}");
            return;
        }

        try
        {
            // PdfBookmarkEditor does not implement IDisposable, so a plain instance is sufficient
            PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

            // Bind the source PDF document
            bookmarkEditor.BindPdf(inputPdf);

            // Import the first batch of bookmarks (XML format)
            bookmarkEditor.ImportBookmarksWithXML(batch1Xml);

            // Import the second batch of bookmarks (will be appended)
            bookmarkEditor.ImportBookmarksWithXML(batch2Xml);

            // Save the PDF with the combined bookmark structure
            bookmarkEditor.Save(outputPdf);

            Console.WriteLine($"Bookmarks merged and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
