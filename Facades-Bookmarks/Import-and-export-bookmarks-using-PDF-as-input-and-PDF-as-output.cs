using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string bookmarksXml = "bookmarks.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use PdfBookmarkEditor to work with bookmarks.
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Load the PDF document.
            editor.BindPdf(inputPdf);

            // Export existing bookmarks to an XML file.
            editor.ExportBookmarksToXML(bookmarksXml);

            // Import bookmarks from the XML file back into the document.
            editor.ImportBookmarksWithXML(bookmarksXml);

            // Save the modified PDF (output will be a PDF regardless of extension).
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Bookmarks exported to '{bookmarksXml}' and re-imported. Output saved to '{outputPdf}'.");
    }
}