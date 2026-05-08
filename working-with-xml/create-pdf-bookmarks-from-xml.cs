using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfInputPath  = "input.pdf";
        const string xmlInputPath  = "outline.xml";
        const string pdfOutputPath = "output.pdf";

        if (!File.Exists(pdfInputPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfInputPath}");
            return;
        }

        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlInputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(pdfInputPath))
        {
            // Load the XML that defines the outline hierarchy
            XDocument xDoc = XDocument.Load(xmlInputPath);
            XElement rootElement = xDoc.Root;
            if (rootElement == null)
            {
                Console.Error.WriteLine("XML does not contain a root element.");
                return;
            }

            // Clear any existing outlines (optional)
            doc.Outlines.Delete();

            // Recursively build outlines based on XML nesting
            foreach (XElement element in rootElement.Elements())
            {
                AddOutlineFromXml(element, doc);
            }

            // Save the modified PDF
            doc.Save(pdfOutputPath);
            Console.WriteLine($"PDF with custom outline saved to '{pdfOutputPath}'.");
        }
    }

    // Recursively creates outline items from an XML element.
    // Each XML element's name (or a "title" attribute if present) becomes the bookmark title.
    // Child XML elements become child bookmarks.
    private static void AddOutlineFromXml(XElement xmlElement, Document doc, OutlineItemCollection parent = null)
    {
        // Determine the title for the bookmark
        string title = (string)xmlElement.Attribute("title") ?? xmlElement.Name.LocalName;

        // Create a new outline item; the constructor requires the root OutlineCollection.
        OutlineItemCollection outlineItem = new OutlineItemCollection(doc.Outlines)
        {
            Title = title,
            // Example destination: first page of the document.
            // Adjust as needed (e.g., based on page numbers stored in XML).
            Destination = new GoToAction(doc.Pages[1])
        };

        // Attach the new item to its parent or to the document root.
        if (parent == null)
        {
            // Top‑level bookmark
            doc.Outlines.Add(outlineItem);
        }
        else
        {
            // Child bookmark
            parent.Add(outlineItem);
        }

        // Recurse for any nested elements to build deeper hierarchy levels.
        foreach (XElement child in xmlElement.Elements())
        {
            AddOutlineFromXml(child, doc, outlineItem);
        }
    }
}