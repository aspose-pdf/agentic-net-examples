using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for XYZExplicitDestination and other action types

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";      // existing PDF to which outlines will be added
        const string xmlPath   = "sections.xml";   // XML file containing section titles and page numbers
        const string outputPdf = "output_with_outlines.pdf";

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

        // Load the PDF document inside a using block (lifecycle rule)
        using (Document doc = new Document(pdfPath))
        {
            // Load the XML that defines the sections.
            // Expected format:
            // <Sections>
            //   <Section Title="Introduction" Page="1" />
            //   <Section Title="Chapter 1"   Page="5" />
            //   ...
            // </Sections>
            XDocument xdoc = XDocument.Load(xmlPath);
            if (xdoc.Root == null)
            {
                Console.Error.WriteLine("XML file does not contain a root element.");
                return;
            }

            foreach (XElement sec in xdoc.Root.Elements("Section"))
            {
                // Safely read attributes – they may be missing or malformed.
                string title = (string)sec.Attribute("Title");
                int? pageNumberNullable = (int?)sec.Attribute("Page");

                if (string.IsNullOrWhiteSpace(title) || pageNumberNullable == null)
                {
                    Console.Error.WriteLine("Section element is missing Title or Page attribute. Skipping.");
                    continue;
                }

                int pageNumber = pageNumberNullable.Value;

                // Validate page number
                if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                {
                    Console.Error.WriteLine($"Invalid page number {pageNumber} for title \"{title}\". Skipping.");
                    continue;
                }

                // Create a new outline item linked to the specified page.
                // The constructor requires the parent OutlineCollection.
                OutlineItemCollection outlineItem = new OutlineItemCollection(doc.Outlines)
                {
                    Title = title,
                    // Destination points to the top‑left of the page with default zoom (1.0)
                    Destination = new XYZExplicitDestination(doc.Pages[pageNumber], 0, 0, 1)
                };

                // Add the outline item to the document's outline collection.
                doc.Outlines.Add(outlineItem);
            }

            // Save the modified PDF (lifecycle rule)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with custom outlines: {outputPdf}");
    }
}
