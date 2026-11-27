using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xmlPath = "extracted_titles.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // Bind the PDF to the bookmark editor
            using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
            {
                bookmarkEditor.BindPdf(pdfPath);

                // Extract the collection of bookmarks (facade model)
                var bookmarks = bookmarkEditor.ExtractBookmarks();

                // Build an XML document with the bookmark titles
                XElement rootElement = new XElement("Bookmarks");

                foreach (var bookmark in bookmarks)
                {
                    // Each Bookmark facade has a Title property
                    rootElement.Add(new XElement("Title", bookmark.Title));
                }

                XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), rootElement);
                doc.Save(xmlPath);
            }

            Console.WriteLine($"Bookmark titles extracted to '{xmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
