using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xmlPath = "structure.xml";
        const string outputPath = "output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(pdfPath))
            {
                // Ensure at least one page exists for outline destinations
                if (doc.Pages.Count == 0)
                {
                    doc.Pages.Add();
                }

                // Load the XML structure
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);

                // Build outline hierarchy based on XML nesting
                ProcessXmlNode(xmlDoc.DocumentElement, doc, 1);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF with outlines saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Handles the root element – adds the first‑level outline items to doc.Outlines
    static void ProcessXmlNode(XmlNode node, Document doc, int pageNumber)
    {
        if (node == null) return;

        // Create an outline item for the current XML node
        OutlineItemCollection item = new OutlineItemCollection(doc.Outlines);
        item.Title = GetNodeTitle(node);
        item.Destination = new XYZExplicitDestination(
            doc.Pages[pageNumber],
            0,                                   // left
            doc.Pages[pageNumber].MediaBox.Height, // top
            1);                                 // zoom

        // Add the item to the document's root outline collection
        doc.Outlines.Add(item);

        // Recursively process child elements
        foreach (XmlNode child in node.ChildNodes)
        {
            if (child.NodeType == XmlNodeType.Element)
            {
                ProcessXmlChildNode(child, item, doc, pageNumber);
            }
        }
    }

    // Handles nested elements – adds them as sub‑items of the supplied parent outline item
    static void ProcessXmlChildNode(XmlNode node, OutlineItemCollection parentItem, Document doc, int pageNumber)
    {
        if (node == null) return;

        OutlineItemCollection item = new OutlineItemCollection(doc.Outlines);
        item.Title = GetNodeTitle(node);
        item.Destination = new XYZExplicitDestination(
            doc.Pages[pageNumber],
            0,
            doc.Pages[pageNumber].MediaBox.Height,
            1);

        // Nest under the parent outline item
        parentItem.Add(item);

        // Recurse for deeper levels
        foreach (XmlNode child in node.ChildNodes)
        {
            if (child.NodeType == XmlNodeType.Element)
            {
                ProcessXmlChildNode(child, item, doc, pageNumber);
            }
        }
    }

    // Helper to obtain a readable title from an XML node
    static string GetNodeTitle(XmlNode node)
    {
        if (node.Attributes != null && node.Attributes["title"] != null)
        {
            return node.Attributes["title"].Value;
        }
        return node.Name;
    }
}