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
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        try
        {
            // PdfBookmarkEditor implements IDisposable, so use a using block.
            using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
            {
                // Bind the existing PDF file. No modifications are performed.
                editor.BindPdf(inputPdf);

                // Export the bookmarks to an XML file.
                editor.ExportBookmarksToXML(outputXml);
            }

            Console.WriteLine($"Bookmarks exported to '{outputXml}'. Source PDF remains unchanged.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}