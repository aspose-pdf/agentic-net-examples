using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for XYZExplicitDestination

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";   // source XML file
        const string outputPdf = "output.pdf"; // PDF with outline

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load XML document
        XDocument xDoc = XDocument.Load(xmlPath);

        // Create a new PDF document with a single blank page (required for destinations)
        using (Document pdf = new Document())
        {
            pdf.Pages.Add(); // page 1 will be the target of all outline entries

            // Build outline hierarchy based on XML element tree
            // Root outline (bookmarks pane title)
            OutlineItemCollection rootOutline = new OutlineItemCollection(pdf.Outlines)
            {
                Title = "Document Outline",
                Destination = new XYZExplicitDestination(pdf.Pages[1], 0, 0, 1)
            };
            pdf.Outlines.Add(rootOutline);

            // Recursively add child outline items for each top‑level XML element
            foreach (XElement element in xDoc.Root.Elements())
            {
                AddOutlineItem(rootOutline, element, pdf, 1);
            }

            // Save the PDF
            pdf.Save(outputPdf);
        }

        Console.WriteLine($"PDF with hierarchical outline saved to '{outputPdf}'.");
    }

    // Recursively creates outline items mirroring the XML structure
    static void AddOutlineItem(OutlineItemCollection parent, XElement xmlElement, Document pdf, int pageNumber)
    {
        // Create a new outline entry under the parent
        OutlineItemCollection child = new OutlineItemCollection(pdf.Outlines)
        {
            Title = xmlElement.Name.LocalName,
            Destination = new XYZExplicitDestination(pdf.Pages[pageNumber], 0, 0, 1)
        };
        parent.Add(child);

        // Process child XML elements recursively
        foreach (XElement sub in xmlElement.Elements())
        {
            AddOutlineItem(child, sub, pdf, pageNumber);
        }
    }
}
