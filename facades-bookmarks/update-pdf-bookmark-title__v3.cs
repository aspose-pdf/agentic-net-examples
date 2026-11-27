using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string tempXmlPath   = "temp.xml";
        const string outputXmlPath = "updated.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // PdfBookmarkEditor implements IDisposable in recent versions, so we can safely use a using block.
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            // Bind the PDF document to the editor.
            bookmarkEditor.BindPdf(inputPdfPath);

            // Export current bookmarks to a temporary XML file.
            bookmarkEditor.ExtractBookmarks(tempXmlPath);

            // Load the exported XML.
            XDocument xDoc = XDocument.Load(tempXmlPath);

            // Find the bookmark element with Title="OldTitle" and change it to "NewTitle".
            var bookmark = xDoc.Descendants()
                               .FirstOrDefault(e => (string)e.Attribute("Title") == "OldTitle");

            if (bookmark != null)
            {
                bookmark.SetAttributeValue("Title", "NewTitle");
            }
            else
            {
                Console.WriteLine("Bookmark with title 'OldTitle' not found.");
            }

            // Save the modified bookmarks to the final XML file.
            xDoc.Save(outputXmlPath);

            // Import the modified bookmarks back into the PDF.
            bookmarkEditor.ImportBookmarksWithXML(outputXmlPath);

            // Persist the changes to the PDF file.
            bookmarkEditor.Save("output.pdf");
        }

        Console.WriteLine($"Bookmarks updated and exported to '{outputXmlPath}'.");
    }
}
