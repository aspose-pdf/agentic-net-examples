using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string bookmarksXml = "export_before.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        try
        {
            // Bind the PDF, export its bookmarks, delete them, and save the result.
            using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
            {
                // Bind the source PDF.
                editor.BindPdf(inputPdf);

                // Export existing bookmarks to an XML file.
                // ExportBookmarksToXML is an *instance* method – it works on the bound PDF.
                editor.ExportBookmarksToXML(bookmarksXml);
                Console.WriteLine($"Bookmarks exported to '{bookmarksXml}'.");

                // Delete all bookmarks (parameter‑less overload removes everything).
                editor.DeleteBookmarks();

                // Save the PDF without bookmarks.
                editor.Save(outputPdf);
                Console.WriteLine($"PDF without bookmarks saved to '{outputPdf}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
