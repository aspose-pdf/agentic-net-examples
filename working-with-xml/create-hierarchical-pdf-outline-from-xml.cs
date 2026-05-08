using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // PDF to which the outline will be added
        const string xmlPath       = "structure.xml"; // XML describing the hierarchy
        const string outputPdfPath = "output_with_outline.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Load the XML that defines the hierarchical structure
            XDocument xmlDoc = XDocument.Load(xmlPath);
            XElement rootElement = xmlDoc.Root;
            if (rootElement == null)
            {
                Console.Error.WriteLine("XML does not contain a root element.");
                return;
            }

            // Create a top‑level outline item for the XML root
            OutlineItemCollection rootOutline = new OutlineItemCollection(pdfDoc.Outlines)
            {
                Title = rootElement.Name.LocalName,
                // Example: link to the first page; adjust as needed (e.g., use an attribute to map pages)
                Destination = new GoToAction(pdfDoc.Pages[1])
            };
            pdfDoc.Outlines.Add(rootOutline);

            // Recursively add child outline items
            AddChildOutlines(rootOutline, rootElement, pdfDoc);

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
            Console.WriteLine($"PDF with hierarchical outline saved to '{outputPdfPath}'.");
        }
    }

    // Recursively creates outline items for each XML element and attaches them to the parent outline item
    private static void AddChildOutlines(OutlineItemCollection parentOutline, XElement xmlElement, Document pdfDoc)
    {
        foreach (XElement child in xmlElement.Elements())
        {
            // Create a new outline item for the child element
            OutlineItemCollection childOutline = new OutlineItemCollection(pdfDoc.Outlines)
            {
                Title = child.Name.LocalName,
                // If the XML element has a "page" attribute, use it; otherwise default to page 1
                Destination = GetDestinationForElement(child, pdfDoc)
            };

            // Attach the child outline to its parent
            parentOutline.Add(childOutline);

            // Recurse to handle deeper levels
            AddChildOutlines(childOutline, child, pdfDoc);
        }
    }

    // Determines the destination for an outline item.
    // Looks for a "page" attribute on the XML element; if present, links to that page.
    // Falls back to the first page if the attribute is missing or invalid.
    private static GoToAction GetDestinationForElement(XElement element, Document pdfDoc)
    {
        int pageNumber = 1; // default page
        XAttribute pageAttr = element.Attribute("page");
        if (pageAttr != null && int.TryParse(pageAttr.Value, out int parsed) && parsed >= 1 && parsed <= pdfDoc.Pages.Count)
        {
            pageNumber = parsed;
        }

        // Create a GoToAction that jumps to the specified page
        return new GoToAction(pdfDoc.Pages[pageNumber]);
    }
}