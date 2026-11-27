using System;
using System.IO;
using System.Xml;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputXmlPath = "count_report.xml";

        // Verify the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Bind the PDF to the bookmark editor
            using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
            {
                bookmarkEditor.BindPdf(inputPdfPath);

                // Extract the bookmark collection (correct method is ExtractBookmarks)
                var bookmarks = bookmarkEditor.ExtractBookmarks();

                // Count the bookmarks (null‑safe)
                int bookmarkCount = bookmarks?.Count ?? 0;

                // Build a simple XML report
                XmlDocument xmlDoc = new XmlDocument();
                XmlElement root = xmlDoc.CreateElement("BookmarkReport");
                xmlDoc.AppendChild(root);

                XmlElement countElement = xmlDoc.CreateElement("Count");
                countElement.InnerText = bookmarkCount.ToString();
                root.AppendChild(countElement);

                // Save the XML report
                xmlDoc.Save(outputXmlPath);

                Console.WriteLine($"Bookmark count ({bookmarkCount}) written to '{outputXmlPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
