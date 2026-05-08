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
        const string xmlPath = "headings.xml";
        const string outputPdf = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load heading hierarchy from XML.
        XDocument xDoc = XDocument.Load(xmlPath);
        // Expected XML format:
        // <Headings>
        //   <Heading level="1">Chapter 1</Heading>
        //   <Heading level="2">Section 1.1</Heading>
        //   ...
        // </Headings>

        // Create a new PDF document.
        using (Document doc = new Document())
        {
            // Add a page that will hold the Table of Contents.
            Page tocPage = doc.Pages.Add();
            // Configure the TOC page info. Title expects a TextFragment.
            tocPage.TocInfo = new TocInfo
            {
                Title = new TextFragment("Table of Contents")
            };

            // Access tagged‑content API.
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Document with Table of Contents");

            // Root of the logical structure tree.
            StructureElement root = tagged.RootElement;

            // Create the TOC structure element.
            TOCElement tocElement = tagged.CreateTOCElement();
            tocElement.Title = "Table of Contents";
            root.AppendChild(tocElement); // Append TOC to the root.

            // Iterate over heading elements in the XML.
            foreach (XElement headingNode in xDoc.Descendants("Heading"))
            {
                // Read level attribute (optional, default to 1).
                int level = 1;
                XAttribute levelAttr = headingNode.Attribute("level");
                if (levelAttr != null && int.TryParse(levelAttr.Value, out int parsed))
                    level = parsed;

                string headingText = headingNode.Value?.Trim() ?? string.Empty;
                if (string.IsNullOrEmpty(headingText))
                    continue; // Skip empty headings.

                // Create a TOCI (Table of Contents Item) element.
                TOCIElement tocItem = tagged.CreateTOCIElement();
                tocItem.ActualText = headingText; // Visible text of the TOC entry.
                tocItem.Language = "en-US";
                tocElement.AppendChild(tocItem); // Add entry to the TOC.

                // Add a content page for this heading (optional but useful for page numbers).
                Page contentPage = doc.Pages.Add();
                // Simple visual representation of the heading.
                TextFragment tf = new TextFragment(headingText);
                // Adjust font size based on heading level (example logic).
                tf.TextState.FontSize = 14 - (level - 1) * 2;
                tf.TextState.Font = FontRepository.FindFont("Helvetica");
                contentPage.Paragraphs.Add(tf);
            }

            // Save the PDF.
            doc.Save(outputPdf);
            Console.WriteLine($"PDF with TOC saved to '{outputPdf}'.");
        }
    }
}