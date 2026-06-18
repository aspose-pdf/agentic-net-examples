using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xmlPath = "headings.xml";
        const string outputPath = "output_with_toc.pdf";

        if (!File.Exists(pdfPath) || !File.Exists(xmlPath))
        {
            Console.Error.WriteLine("Missing input PDF or XML file.");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(pdfPath))
        {
            // Load heading hierarchy from XML
            // Expected XML format:
            // <headings>
            //   <heading level="1" title="Chapter 1" page="1" />
            //   <heading level="2" title="Section 1.1" page="2" />
            //   ...
            // </headings>
            XDocument xdoc = XDocument.Load(xmlPath);
            var headingElements = xdoc.Root?.Elements("heading");

            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Create the top‑level TOC element
            TOCElement toc = tagged.CreateTOCElement();
            // Optional: set a visible title for the TOC element itself
            toc.Title = "Table of Contents";

            // Build TOC entries (TOCI elements) from the XML headings
            if (headingElements != null)
            {
                foreach (var h in headingElements)
                {
                    string title = (string)h.Attribute("title") ?? string.Empty;
                    string pageNumber = (string)h.Attribute("page") ?? "0";

                    // Create a TOCI element for this heading
                    TOCIElement toci = tagged.CreateTOCIElement();
                    toci.ActualText = title; // label of the entry

                    // Create a reference element that holds the page number
                    var reference = tagged.CreateReferenceElement();
                    reference.ActualText = pageNumber;

                    // Attach the reference to the TOCI entry
                    toci.AppendChild(reference);

                    // Attach the TOCI entry to the TOC
                    toc.AppendChild(toci);
                }
            }

            // Append the completed TOC to the document's root structure element
            StructureElement root = tagged.RootElement;
            root.AppendChild(toc);

            // Configure TOC appearance on the first page (or any dedicated page)
            Page tocPage = doc.Pages[1];
            tocPage.TocInfo = new TocInfo
            {
                Title = new TextFragment("Table of Contents"),
                IsShowPageNumbers = true,
                CopyToOutlines = false
            };

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with generated TOC saved to '{outputPath}'.");
    }
}
