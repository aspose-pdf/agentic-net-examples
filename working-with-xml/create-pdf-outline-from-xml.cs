using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string pdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML file and convert it to PDF
        XmlLoadOptions loadOpts = new XmlLoadOptions();
        using (Document doc = new Document(xmlPath, loadOpts))
        {
            // Parse the XML to build an outline that mirrors its nesting depth
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            BuildOutline(doc, xmlDoc.DocumentElement, 0);

            // Save the resulting PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PDF with hierarchical outline saved to '{pdfPath}'.");
    }

    // Recursively creates outline items; indentation reflects XML nesting level
    static void BuildOutline(Document doc, XmlNode node, int depth)
    {
        if (node == null) return;

        // Title includes spaces proportional to depth for visual hierarchy
        string title = new string(' ', depth * 2) + node.Name;

        // Create a new outline item and add it to the document's outline collection
        OutlineItemCollection outlineItem = new OutlineItemCollection(doc.Outlines)
        {
            Title = title,
            Open = true // expand the item by default
        };
        doc.Outlines.Add(outlineItem);

        // Process child elements recursively, increasing the depth
        foreach (XmlNode child in node.ChildNodes)
        {
            if (child.NodeType == XmlNodeType.Element)
                BuildOutline(doc, child, depth + 1);
        }
    }
}