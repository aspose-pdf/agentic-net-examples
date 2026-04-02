using System;
using System.Xml;
using Aspose.Pdf;

namespace AsposePdfOutlineFromXml
{
    class Program
    {
        static void Main(string[] args)
        {
            // Sample XML representing a hierarchical structure
            string xmlContent = "<root><section name='Chapter 1'><subsection name='Section 1.1'/><subsection name='Section 1.2'/></section><section name='Chapter 2'/></root>";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlContent);
            XmlNode rootNode = xmlDoc.DocumentElement;

            // Create a new PDF document
            using (Document pdfDoc = new Document())
            {
                // Add a blank page (required for a PDF to have content)
                pdfDoc.Pages.Add();

                // Get the root outline collection
                OutlineCollection outlines = pdfDoc.Outlines;

                // Recursively add outline items based on XML elements
                foreach (XmlNode childNode in rootNode.ChildNodes)
                {
                    if (childNode.NodeType == XmlNodeType.Element)
                    {
                        AddOutlineItem(pdfDoc, outlines, childNode, null);
                    }
                }

                // Save the PDF with the generated outline hierarchy
                pdfDoc.Save("output.pdf");
            }
        }

        // Recursive helper to create outline items
        private static void AddOutlineItem(Document pdfDoc, OutlineCollection outlines, XmlNode xmlNode, OutlineItemCollection parentItem)
        {
            // Create a new outline item using the root outline collection
            OutlineItemCollection newItem = new OutlineItemCollection(outlines);

            // Use the "name" attribute if present; otherwise fall back to the element name
            string title = xmlNode.Attributes != null && xmlNode.Attributes["name"] != null
                ? xmlNode.Attributes["name"].Value
                : xmlNode.Name;
            newItem.Title = title;

            // Attach the new item to its parent or to the top‑level collection
            if (parentItem == null)
            {
                outlines.Add(newItem);
            }
            else
            {
                parentItem.Add(newItem);
            }

            // Process child elements recursively
            foreach (XmlNode childNode in xmlNode.ChildNodes)
            {
                if (childNode.NodeType == XmlNodeType.Element)
                {
                    AddOutlineItem(pdfDoc, outlines, childNode, newItem);
                }
            }
        }
    }
}
