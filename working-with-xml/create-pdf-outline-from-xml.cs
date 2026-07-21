using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for GoToAction

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

        // Load XML and convert it to PDF using XmlLoadOptions (required for XML input)
        using (Document pdfDoc = new Document(xmlPath, new XmlLoadOptions()))
        {
            // Build outline hierarchy based on XML nesting
            OutlineCollection outlines = pdfDoc.Outlines;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            // Start recursion from the document element (if it exists)
            if (xmlDoc.DocumentElement != null)
            {
                AddOutlineFromXmlNode(xmlDoc.DocumentElement, outlines, pdfDoc, null);
            }

            // Save the resulting PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with custom outline saved to '{outputPdfPath}'.");
    }

    /// <summary>
    /// Recursively creates outline items from an XML node.
    /// </summary>
    /// <param name="xmlNode">Current XML node.</param>
    /// <param name="outlines">Root outline collection (used only for top‑level items).</param>
    /// <param name="doc">PDF document (used to create destinations).</param>
    /// <param name="parentItem">Parent outline item; null for top‑level entries.</param>
    private static void AddOutlineFromXmlNode(XmlNode xmlNode,
                                              OutlineCollection outlines,
                                              Document doc,
                                              OutlineItemCollection? parentItem)
    {
        if (xmlNode == null) return;

        // Create a new outline item. The constructor requires the root OutlineCollection.
        OutlineItemCollection outlineItem = new OutlineItemCollection(outlines)
        {
            Title = xmlNode.Name,
            // For demonstration, link every outline entry to the first page.
            Action = new GoToAction(doc.Pages[1])
        };

        // Attach the item to the appropriate parent.
        if (parentItem == null)
        {
            // Top‑level item
            outlines.Add(outlineItem);
        }
        else
        {
            // Child item
            parentItem.Add(outlineItem);
        }

        // Recurse for child XML nodes (only element nodes)
        foreach (XmlNode child in xmlNode.ChildNodes)
        {
            if (child.NodeType == XmlNodeType.Element)
            {
                AddOutlineFromXmlNode(child, outlines, doc, outlineItem);
            }
        }
    }
}
