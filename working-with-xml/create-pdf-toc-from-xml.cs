using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text; // needed for TextFragment

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";      // existing PDF (can be empty)
        const string xmlPath       = "headings.xml";   // XML with heading hierarchy
        const string outputPdfPath = "output_with_toc.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the source PDF (or create a new one if the file does not exist)
        using (Document doc = File.Exists(inputPdfPath) ? new Document(inputPdfPath) : new Document())
        {
            // Ensure the document is disposed properly (lifecycle rule)

            // -----------------------------------------------------------------
            // 1. Prepare tagged content
            // -----------------------------------------------------------------
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(outputPdfPath));

            // -----------------------------------------------------------------
            // 2. Create a dedicated page for the Table of Contents
            // -----------------------------------------------------------------
            Page tocPage = doc.Pages.Add();                     // add a new page at the end
            tocPage.TocInfo = new TocInfo
            {
                Title = new TextFragment("Table of Contents"), // Title expects TextFragment
                IsShowPageNumbers = true,
                CopyToOutlines = true
            };

            // -----------------------------------------------------------------
            // 3. Build the TOC structure
            // -----------------------------------------------------------------
            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create the top‑level TOC element and attach it to the root
            TOCElement tocElement = tagged.CreateTOCElement();
            root.AppendChild(tocElement);

            // Load heading hierarchy from the XML file
            XDocument xDoc = XDocument.Load(xmlPath);

            // Example XML format:
            // <headings>
            //   <heading level="1">Chapter 1</heading>
            //   <heading level="2">Section 1.1</heading>
            //   <heading level="2">Section 1.2</heading>
            //   <heading level="1">Chapter 2</heading>
            // </headings>

            foreach (XElement headingNode in xDoc.Descendants("heading"))
            {
                // Extract level and text
                int level = (int?)headingNode.Attribute("level") ?? 1;
                string headingText = headingNode.Value.Trim();

                // Create a TOC item (TOCI element)
                TOCIElement toci = tagged.CreateTOCIElement();

                // Use ActualText to store the visible label of the TOC entry
                toci.ActualText = headingText;

                // Append the TOCI element to the TOC container
                tocElement.AppendChild(toci);
            }

            // -----------------------------------------------------------------
            // 4. Save the modified PDF
            // -----------------------------------------------------------------
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with Table of Contents saved to '{outputPdfPath}'.");
    }
}
