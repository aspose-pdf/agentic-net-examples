using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // Bookmark class resides here

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";
        const string importXml = "import1.xml";
        const string outputPdf = "output.pdf";

        // Verify input files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(importXml))
        {
            Console.Error.WriteLine($"Bookmark XML not found: {importXml}");
            return;
        }

        // PdfBookmarkEditor does NOT implement IDisposable, so we instantiate it directly
        PdfBookmarkEditor editor = new PdfBookmarkEditor();

        // Bind the existing PDF file
        editor.BindPdf(pdfPath);

        // Import bookmarks from the XML file (correct method name)
        editor.ImportBookmarksWithXML(importXml);

        // Rename the bookmark titled "SectionA" to "SectionAlpha"
        // The facade provides ModifyBookmarks(sourceTitle, destinationTitle)
        editor.ModifyBookmarks("SectionA", "SectionAlpha");

        // Save the updated PDF
        editor.Save(outputPdf);

        Console.WriteLine($"PDF saved with updated bookmarks to '{outputPdf}'.");
    }
}
