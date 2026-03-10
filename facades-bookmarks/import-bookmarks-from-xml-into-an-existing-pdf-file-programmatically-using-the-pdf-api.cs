using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string bookmarksXml = "bookmarks.xml";
        const string outputPdf = "output.pdf";

        // Verify that the source PDF and the XML file exist
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

        // Use PdfBookmarkEditor (a facade) to work with bookmarks
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            // Load the existing PDF document
            editor.BindPdf(inputPdf);

            // Import bookmarks from the provided XML file
            editor.ImportBookmarksWithXML(bookmarksXml);

            // Save the updated PDF to a new file
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Bookmarks imported successfully. Output saved to '{outputPdf}'.");
    }
}