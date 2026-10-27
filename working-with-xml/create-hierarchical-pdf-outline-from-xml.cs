using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // Required for XYZExplicitDestination overload

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string xmlPath = "structure.xml";
        const string outputPdf = "outlined.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML document
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlPath);

        // Create a new PDF document inside a using block (deterministic disposal)
        using (Document pdf = new Document())
        {
            // Add a single blank page – all outline entries will point to this page
            Page page = pdf.Pages.Add();

            // Recursively build the outline hierarchy from the XML root element
            XmlElement rootElement = xmlDoc.DocumentElement;
            if (rootElement != null)
            {
                AddOutlineItem(rootElement, pdf, null, page);
            }

            // Save the PDF
            pdf.Save(outputPdf);
        }

        Console.WriteLine($"PDF with hierarchical outline saved to '{outputPdf}'.");
    }

    /// <summary>
    /// Recursively creates outline items that mirror the XML element hierarchy.
    /// </summary>
    /// <param name="xmlNode">Current XML element.</param>
    /// <param name="pdf">The PDF document.</param>
    /// <param name="parentOutline">Parent outline item; null for top‑level.</param>
    /// <param name="targetPage">Page to which the outline entries will navigate.</param>
    private static void AddOutlineItem(XmlElement xmlNode, Document pdf, OutlineItemCollection? parentOutline, Page targetPage)
    {
        // Create an outline entry with the element name as its title
        OutlineItemCollection outline = new OutlineItemCollection(pdf.Outlines)
        {
            Title = xmlNode.Name,
            // Use the XYZExplicitDestination overload that takes a page number (int)
            Destination = new XYZExplicitDestination(targetPage.Number, 0, targetPage.PageInfo.Height, 1.0f)
        };

        // Attach the outline to the document hierarchy
        if (parentOutline == null)
        {
            // Top‑level outline items are added to the document's root outline collection
            pdf.Outlines.Add(outline);
        }
        else
        {
            // Child outline items are added to the parent's collection
            parentOutline.Add(outline);
        }

        // Process child elements recursively
        foreach (XmlNode child in xmlNode.ChildNodes)
        {
            if (child.NodeType == XmlNodeType.Element)
            {
                AddOutlineItem((XmlElement)child, pdf, outline, targetPage);
            }
        }
    }
}
