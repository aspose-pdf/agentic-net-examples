using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputXml = "bookmarks.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // PdfBookmarkEditor is a facade for bookmark operations.
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Load the PDF document into the editor.
            editor.BindPdf(inputPdf);

            // Export all bookmarks to an XML file.
            editor.ExportBookmarksToXML(outputXml);
        }

        Console.WriteLine($"Bookmarks have been exported to '{outputXml}'.");
    }
}