using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputXml = "bookmarks.xml";

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the bookmark editor, bind the PDF, and export bookmarks to XML
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPdf);
        editor.ExportBookmarksToXML(outputXml);
        editor.Close(); // Release resources held by the editor

        Console.WriteLine($"Bookmarks successfully exported to '{outputXml}'.");
    }
}