using System;
using System.Collections.Generic;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

namespace PdfOutlineFromXmlExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a simple PDF that will receive the outlines.
            using (Document createDoc = new Document())
            {
                // Add three pages – enough for demonstration.
                createDoc.Pages.Add();
                createDoc.Pages.Add();
                createDoc.Pages.Add();
                // Save the sample PDF.
                createDoc.Save("input.pdf");
            }

            // Step 2: Define a small XML string whose hierarchy will be reflected in the PDF outline.
            string xmlContent = "<book><chapter><section/></chapter><chapter/></book>";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlContent);

            // Step 3: Open the PDF and build the outline hierarchy.
            using (Document doc = new Document("input.pdf"))
            {
                OutlineCollection outlines = doc.Outlines;
                // Stack that keeps the last outline item at each depth level.
                List<OutlineItemCollection> parentStack = new List<OutlineItemCollection>();

                // Recursive method to walk the XML tree.
                void AddNode(XmlNode node, int depth)
                {
                    // Create a new outline item for the current XML element.
                    OutlineItemCollection outlineItem = new OutlineItemCollection(outlines);
                    outlineItem.Title = node.Name;

                    // Choose a destination page – map depth to page number (1‑based).
                    int pageNumber = depth + 1;
                    if (pageNumber > doc.Pages.Count)
                    {
                        pageNumber = doc.Pages.Count;
                    }
                    outlineItem.Action = new GoToAction(doc.Pages[pageNumber]);

                    // Attach the outline item to the correct parent.
                    if (depth == 0)
                    {
                        outlines.Add(outlineItem);
                    }
                    else
                    {
                        OutlineItemCollection parentItem = parentStack[depth - 1];
                        parentItem.Add(outlineItem);
                    }

                    // Ensure the stack has a slot for the current depth.
                    if (parentStack.Count > depth)
                    {
                        parentStack[depth] = outlineItem;
                    }
                    else
                    {
                        parentStack.Add(outlineItem);
                    }

                    // Recurse into child elements.
                    foreach (XmlNode child in node.ChildNodes)
                    {
                        if (child.NodeType == XmlNodeType.Element)
                        {
                            AddNode(child, depth + 1);
                        }
                    }
                }

                // Start recursion from the document element.
                AddNode(xmlDoc.DocumentElement, 0);

                // Save the PDF with the generated outline hierarchy.
                doc.Save("output.pdf");
            }
        }
    }
}
