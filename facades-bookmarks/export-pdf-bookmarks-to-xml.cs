using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXml = "bookmarks.xml";

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the bookmark editor facade
        PdfBookmarkEditor editor = new PdfBookmarkEditor();

        // Load the PDF document into the editor
        editor.BindPdf(inputPdf);

        // Export the complete bookmark hierarchy to an XML file
        editor.ExportBookmarksToXML(outputXml);

        // Release resources held by the facade
        editor.Close();

        Console.WriteLine($"Bookmarks successfully exported to '{outputXml}'.");
    }
}