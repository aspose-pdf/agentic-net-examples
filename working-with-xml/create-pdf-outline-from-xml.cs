using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for GoToAction

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";   // source XML file
        const string outputPdf = "output.pdf"; // resulting PDF with outline

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load XML into a PDF document using BindXml (recommended over constructor)
        using (Document pdfDoc = new Document())
        {
            pdfDoc.BindXml(xmlPath);

            // Parse the XML to obtain its hierarchical structure
            XDocument xDoc = XDocument.Load(xmlPath);
            if (xDoc.Root == null)
            {
                Console.Error.WriteLine("XML does not contain a root element.");
                return;
            }

            // Recursively create outline items matching XML nesting
            ProcessXmlElement(xDoc.Root, null, pdfDoc);

            // Save the PDF with the generated outline hierarchy
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with outline saved to '{outputPdf}'.");
    }

    // Recursive helper to map each XML element to an outline entry
    static void ProcessXmlElement(
        XElement xmlElement,
        OutlineItemCollection? parentOutline,
        Document pdfDoc)
    {
        // Ensure the element is not null (defensive programming)
        if (xmlElement == null) return;

        // Create a new outline item; the constructor requires the root OutlineCollection
        OutlineItemCollection outlineItem = new OutlineItemCollection(pdfDoc.Outlines)
        {
            // Use the element name as the outline title (customize as needed)
            Title = xmlElement.Name.LocalName,
            // Simple navigation: jump to the first page of the PDF
            Destination = new GoToAction(pdfDoc.Pages[1])
        };

        // Attach the outline item to the appropriate parent
        if (parentOutline == null)
        {
            // Top‑level entry
            pdfDoc.Outlines.Add(outlineItem);
        }
        else
        {
            // Child entry under the current parent
            parentOutline.Add(outlineItem);
        }

        // Recurse for each child XML element, using the current outline as the new parent
        foreach (XElement child in xmlElement.Elements())
        {
            ProcessXmlElement(child, outlineItem, pdfDoc);
        }
    }
}
