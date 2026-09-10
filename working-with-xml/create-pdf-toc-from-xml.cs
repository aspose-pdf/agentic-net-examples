using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string xmlPath = "headings.xml";   // XML with heading hierarchy
        const string pdfPath = "input.pdf";      // source PDF (may be empty)
        const string outputPdf = "output_with_toc.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the XML that defines the headings
        XDocument xDoc = XDocument.Load(xmlPath);
        if (xDoc.Root == null)
        {
            Console.Error.WriteLine("XML does not contain a root element.");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(pdfPath))
        {
            // Ensure the document has a tagged structure
            ITaggedContent tagged = doc.TaggedContent;

            // Insert a blank page that will hold the TOC (as the first page)
            doc.Pages.Insert(1); // creates a new empty page at position 1
            Page tocPage = doc.Pages[1];

            // Configure TOC page info (title, page numbers, etc.)
            tocPage.TocInfo = new TocInfo
            {
                Title = new TextFragment("Table of Contents"), // <-- fixed: TextFragment required
                IsShowPageNumbers = true,
                CopyToOutlines = false
            };

            // Create the top‑level TOC element and attach it to the root
            TOCElement tocRoot = tagged.CreateTOCElement();
            tocRoot.Title = "Table of Contents"; // visible title for the TOC element
            StructureElement root = tagged.RootElement;
            root.AppendChild(tocRoot);

            // Recursive helper to process heading elements
            void ProcessHeadingElements(XElement xmlElement, TOCElement parentToc)
            {
                if (xmlElement == null) return;

                foreach (XElement heading in xmlElement.Elements("heading"))
                {
                    // Create a TOC item (TOCI) for this heading
                    TOCIElement tocItem = tagged.CreateTOCIElement();

                    // Use ActualText to store the visible label of the TOC entry
                    tocItem.ActualText = heading.Value?.Trim() ?? string.Empty;

                    // Optionally store the heading level (if needed later)
                    XAttribute levelAttr = heading.Attribute("level");
                    if (levelAttr != null)
                        tocItem.Language = levelAttr.Value; // simple example storage

                    // Append the TOCI element to its parent TOC element
                    parentToc.AppendChild(tocItem);

                    // If the heading contains nested headings, create a nested TOC element
                    if (heading.Elements("heading").Any())
                    {
                        TOCElement nestedToc = tagged.CreateTOCElement();
                        // Optionally give the nested TOC a title (could be the same as the item)
                        nestedToc.Title = tocItem.ActualText;
                        tocItem.AppendChild(nestedToc);
                        ProcessHeadingElements(heading, nestedToc);
                    }
                }
            }

            // Start processing from the root of the XML document
            ProcessHeadingElements(xDoc.Root, tocRoot);

            // Save the updated PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with TOC saved to '{outputPdf}'.");
    }
}
