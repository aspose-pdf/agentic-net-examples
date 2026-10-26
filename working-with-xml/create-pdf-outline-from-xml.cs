using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xmlPath = "structure.xml";
        const string outputPdf = "output.pdf";

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

        // Load the XML that defines the outline hierarchy.
        XDocument xmlDoc = XDocument.Load(xmlPath);
        XElement rootElement = xmlDoc.Root;
        if (rootElement == null)
        {
            Console.Error.WriteLine("XML does not contain a root element.");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Ensure the document has at least one page to link outlines to.
            int targetPageNumber = 1;
            if (pdfDoc.Pages.Count == 0)
            {
                // Add a blank page if the source PDF is empty.
                pdfDoc.Pages.Add();
            }

            // Clear any existing outlines (optional).
            pdfDoc.Outlines.Clear();

            // Build the outline hierarchy based on XML nesting.
            AddTopLevelOutlines(pdfDoc, rootElement, targetPageNumber);

            // Save the modified PDF.
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with custom outline saved to '{outputPdf}'.");
    }

    // Adds top‑level outline items (children of the document root).
    private static void AddTopLevelOutlines(Document pdfDoc, XElement xmlRoot, int pageNumber)
    {
        foreach (XElement child in xmlRoot.Elements())
        {
            // Create an outline item for the current XML element.
            var outlineItem = new OutlineItemCollection(pdfDoc.Outlines)
            {
                Title = child.Attribute("title")?.Value ?? child.Name.LocalName,
                Destination = new XYZExplicitDestination(pdfDoc.Pages[pageNumber], 0, 0, 1)
            };

            // Add the item to the document's root outline collection.
            pdfDoc.Outlines.Add(outlineItem);

            // Recursively add any nested children.
            AddChildOutlines(outlineItem, child, pdfDoc, pageNumber);
        }
    }

    // Recursively adds child outline items to a parent outline item.
    private static void AddChildOutlines(OutlineItemCollection parent, XElement xmlElement, Document pdfDoc, int pageNumber)
    {
        foreach (XElement child in xmlElement.Elements())
        {
            var childItem = new OutlineItemCollection(pdfDoc.Outlines)
            {
                Title = child.Attribute("title")?.Value ?? child.Name.LocalName,
                Destination = new XYZExplicitDestination(pdfDoc.Pages[pageNumber], 0, 0, 1)
            };

            // Add the child to its parent outline item.
            parent.Add(childItem);

            // Recurse further if the child has its own children.
            AddChildOutlines(childItem, child, pdfDoc, pageNumber);
        }
    }
}
