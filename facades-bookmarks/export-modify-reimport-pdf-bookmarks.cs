using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string snapshotXml = "snapshot.xml";
        const string modifiedXml = "modified_snapshot.xml";
        const string updatedSnapshotXml = "updated_snapshot.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Bind the PDF to the bookmark editor and work within a using block so resources are released.
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            bookmarkEditor.BindPdf(inputPdf);

            // Export current bookmarks to snapshot.xml (method name is ExportBookmarksToXML).
            bookmarkEditor.ExportBookmarksToXML(snapshotXml);
            Console.WriteLine($"Bookmarks exported to {snapshotXml}");

            // Load the exported XML, modify a bookmark title, and save to a temporary file.
            XDocument xmlDoc = XDocument.Load(snapshotXml);
            XElement titleElement = xmlDoc.Descendants("Title").FirstOrDefault();
            if (titleElement != null)
            {
                titleElement.Value = "New Bookmark Title";
                xmlDoc.Save(modifiedXml);
                Console.WriteLine($"Modified bookmarks saved to {modifiedXml}");
            }
            else
            {
                Console.WriteLine("No bookmark title found to modify.");
                return;
            }

            // Import the modified bookmarks back into the PDF (method name is ImportBookmarksWithXML).
            bookmarkEditor.ImportBookmarksWithXML(modifiedXml);

            // Export the updated bookmarks to updated_snapshot.xml.
            bookmarkEditor.ExportBookmarksToXML(updatedSnapshotXml);
            Console.WriteLine($"Updated bookmarks exported to {updatedSnapshotXml}");

            // Optionally persist the changes back to the PDF file.
            // bookmarkEditor.Save(inputPdf);
        }
    }
}
