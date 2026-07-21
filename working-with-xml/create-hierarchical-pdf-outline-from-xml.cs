using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for XYZExplicitDestination

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML as a PDF document (Aspose creates a page per XML element)
        using (Document pdfDoc = new Document(xmlPath, new XmlLoadOptions()))
        {
            // Load the XML separately to walk its element hierarchy
            XDocument xDoc = XDocument.Load(xmlPath);
            XElement rootElement = xDoc.Root;
            if (rootElement == null)
            {
                Console.Error.WriteLine("The XML document does not contain a root element.");
                return;
            }

            // Ensure at least one page exists (XmlLoadOptions creates pages, but guard anyway)
            if (pdfDoc.Pages.Count == 0)
                pdfDoc.Pages.Add();

            // Create the top‑level outline (bookmark) for the root element
            OutlineItemCollection rootOutline = new OutlineItemCollection(pdfDoc.Outlines)
            {
                Title = rootElement.Name.LocalName,
                Destination = new XYZExplicitDestination(pdfDoc.Pages[1], 0, 0, 1)
            };
            pdfDoc.Outlines.Add(rootOutline);

            // Recursively add child outline items reflecting XML nesting
            AddChildOutlines(rootOutline, rootElement, pdfDoc);

            // Save the PDF with the hierarchical outline
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with outline saved to '{outputPdfPath}'.");
    }

    // Recursively creates outline items for each XML child element
    static void AddChildOutlines(OutlineItemCollection parentOutline, XElement xmlElement, Document pdfDoc)
    {
        foreach (XElement child in xmlElement.Elements())
        {
            // Create an outline entry for the child element
            OutlineItemCollection childOutline = new OutlineItemCollection(pdfDoc.Outlines)
            {
                Title = child.Name.LocalName,
                Destination = new XYZExplicitDestination(pdfDoc.Pages[1], 0, 0, 1)
            };
            parentOutline.Add(childOutline);

            // Recurse to handle deeper nesting
            AddChildOutlines(childOutline, child, pdfDoc);
        }
    }
}
