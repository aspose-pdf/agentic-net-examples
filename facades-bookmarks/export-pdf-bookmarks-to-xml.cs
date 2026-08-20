using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xmlPath = "bookmarks.xml";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Initialize the bookmark editor, bind the PDF and export its bookmarks to XML
        PdfBookmarkEditor editor = new PdfBookmarkEditor();
        editor.BindPdf(pdfPath);                 // Load the PDF into the facade
        editor.ExportBookmarksToXML(xmlPath);    // Export the complete bookmark list
        editor.Close();                          // Release resources held by the editor

        Console.WriteLine($"Bookmarks successfully exported to '{xmlPath}'.");
    }
}