using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string beforeXml  = "before_import.xml";
        const string addXml      = "add.xml";
        const string outputPdf  = "output.pdf";

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(addXml))
        {
            Console.Error.WriteLine($"Bookmarks XML to import not found: {addXml}");
            return;
        }

        try
        {
            // PdfBookmarkEditor does NOT implement IDisposable, so no using block.
            PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

            // Bind the PDF document to the editor
            bookmarkEditor.BindPdf(inputPdf);

            // Export existing bookmarks to an XML file before importing new ones
            bookmarkEditor.ExportBookmarksToXML(beforeXml);
            Console.WriteLine($"Exported existing bookmarks to '{beforeXml}'.");

            // Import additional bookmarks from the provided XML file
            bookmarkEditor.ImportBookmarksWithXML(addXml);
            Console.WriteLine($"Imported bookmarks from '{addXml}'.");

            // Save the modified PDF with the combined bookmark tree
            bookmarkEditor.Save(outputPdf);
            Console.WriteLine($"Saved updated PDF to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
