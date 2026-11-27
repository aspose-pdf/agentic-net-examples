using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";
        const string exportXmlPath     = "export_initial.xml";
        const string supplementXmlPath = "supplement.xml";
        const string outputPdfPath     = "output.pdf";

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(supplementXmlPath))
        {
            Console.Error.WriteLine($"Supplement XML not found: {supplementXmlPath}");
            return;
        }

        try
        {
            // PdfBookmarkEditor does not implement IDisposable, so no using block needed
            PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor();

            // Bind the PDF document to the editor
            bookmarkEditor.BindPdf(inputPdfPath);

            // Export existing bookmarks to an XML file (correct method name)
            bookmarkEditor.ExportBookmarksToXML(exportXmlPath);
            Console.WriteLine($"Bookmarks exported to: {exportXmlPath}");

            // Import additional bookmarks from the supplement XML (correct method name)
            bookmarkEditor.ImportBookmarksWithXML(supplementXmlPath);
            Console.WriteLine($"Bookmarks imported from: {supplementXmlPath}");

            // Save the modified PDF with the new bookmark structure
            bookmarkEditor.Save(outputPdfPath);
            Console.WriteLine($"Modified PDF saved to: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
