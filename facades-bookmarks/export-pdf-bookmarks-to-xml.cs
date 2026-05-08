using System;
using System.IO;
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

        // Load the PDF, export its bookmarks to an XML file, then release resources.
        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPdf);                 // Initialize the facade with the PDF.
            editor.ExportBookmarksToXML(outputXml);   // Export the complete bookmark list.
            editor.Close();                           // Optional explicit cleanup.
        }

        Console.WriteLine($"Bookmarks exported to '{outputXml}'.");
    }
}