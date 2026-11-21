using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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

        // Load XML document
        XDocument xdoc = XDocument.Load(xmlPath);

        // Create PDF document
        using (Document pdfDoc = new Document())
        {
            // Ensure the PDF has at least one page
            pdfDoc.Pages.Add();

            // Build outline hierarchy from XML root
            AddOutlineItems(pdfDoc, null, xdoc.Root);

            // Save the PDF
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF with hierarchical outline saved to '{pdfPath}'.");
    }

    /// <summary>
    /// Recursively adds outline items for each XML element.
    /// </summary>
    /// <param name="doc">The PDF document.</param>
    /// <param name="parentItem">The parent outline node; null for top‑level items.</param>
    /// <param name="element">Current XML element.</param>
    static void AddOutlineItems(Document doc, OutlineItemCollection parentItem, XElement element)
    {
        // Create a readable title for the outline entry
        string title = element.Name.LocalName;
        if (!element.HasElements && !string.IsNullOrWhiteSpace(element.Value))
        {
            title += $": {element.Value.Trim()}";
        }

        // Create a new outline node (OutlineItemCollection acts as a node)
        OutlineItemCollection node = new OutlineItemCollection(doc.Outlines)
        {
            Title = title,
            // Destination: first page, top‑left corner (zoom = 1)
            Destination = new XYZExplicitDestination(doc.Pages[1], 0, doc.Pages[1].MediaBox.Height, 1)
        };

        // Attach the node to the hierarchy
        if (parentItem == null)
            doc.Outlines.Add(node);   // top‑level bookmark
        else
            parentItem.Add(node);      // child bookmark

        // Recurse for child XML elements
        foreach (XElement child in element.Elements())
        {
            AddOutlineItems(doc, node, child);
        }
    }
}