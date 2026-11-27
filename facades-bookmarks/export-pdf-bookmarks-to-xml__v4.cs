using System;
using System.IO;
using System.Xml;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXml = "titles_pages.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Bind the PDF and extract its bookmarks using the Facade model
        using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
        {
            bookmarkEditor.BindPdf(inputPdf);
            // In the Facade API the collection type is "Bookmarks"
            Bookmarks bookmarks = bookmarkEditor.ExtractBookmarks();

            // Create an XML document to hold the bookmark data
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement root = xmlDoc.CreateElement("Bookmarks");
            xmlDoc.AppendChild(root);

            // Process each top‑level bookmark (recursively handles children)
            foreach (Bookmark bm in bookmarks)
            {
                ProcessBookmark(bm, root);
            }

            // Save the XML file
            xmlDoc.Save(outputXml);
            Console.WriteLine($"Bookmarks exported to '{outputXml}'.");
        }
    }

    // Recursively writes a bookmark and its children to XML
    static void ProcessBookmark(Bookmark bm, XmlElement parentXmlElement)
    {
        XmlDocument doc = parentXmlElement.OwnerDocument;

        XmlElement bmElement = doc.CreateElement("Bookmark");
        bmElement.SetAttribute("Title", bm.Title ?? string.Empty);
        // In the Facade model the page number is stored in the PageNumber property
        int pageNumber = bm.PageNumber > 0 ? bm.PageNumber : -1;
        bmElement.SetAttribute("Page", pageNumber.ToString());

        parentXmlElement.AppendChild(bmElement);

        // Recurse into child bookmarks, if any (ChildItems is the correct collection)
        if (bm.ChildItems != null && bm.ChildItems.Count > 0)
        {
            foreach (Bookmark child in bm.ChildItems)
            {
                ProcessBookmark(child, bmElement);
            }
        }
    }
}