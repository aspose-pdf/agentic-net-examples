using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXml = "bookmarks.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the bookmark editor and bind the PDF document
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(inputPdf);

        // Export the complete bookmark list to an XML file
        editor.ExportBookmarksToXML(outputXml);

        // Release resources held by the editor
        editor.Close();

        Console.WriteLine($"Bookmarks have been exported to '{outputXml}'.");
    }
}