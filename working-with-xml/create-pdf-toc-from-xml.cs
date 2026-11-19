using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string xmlPath = "headings.xml";
        const string outputPdf = "DocumentWithTOC.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load heading hierarchy from XML.
        // Expected format:
        // <Document>
        //   <Heading level="1">Chapter 1</Heading>
        //   <Heading level="2">Section 1.1</Heading>
        //   ...
        // </Document>
        XDocument xDoc = XDocument.Load(xmlPath);
        var headings = xDoc.Root.Elements("Heading");

        // Create a new PDF document.
        using (Document pdfDoc = new Document())
        {
            // ----- Tagged PDF setup -----
            ITaggedContent tagged = pdfDoc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Document with Table of Contents");

            // Root of the logical structure tree.
            StructureElement root = tagged.RootElement;

            // ----- Create TOC page (first page) -----
            Page tocPage = pdfDoc.Pages.Add();
            double yPos = tocPage.PageInfo.Height - 50; // start position
            const double lineHeight = 20;

            // Add a visible TOC heading.
            TextFragment tocHeader = new TextFragment("Table of Contents");
            tocHeader.Position = new Position(50, yPos);
            tocHeader.TextState.FontSize = 18;
            tocHeader.TextState.FontStyle = FontStyles.Bold;
            tocPage.Paragraphs.Add(tocHeader);
            yPos -= lineHeight;

            // Create a structure element for the TOC.
            ParagraphElement tocStruct = tagged.CreateParagraphElement();
            tocStruct.SetText("Table of Contents");
            root.AppendChild(tocStruct);

            // ----- Process each heading -----
            int pageNumber = 2; // TOC occupies page 1
            foreach (var headingElem in headings)
            {
                // Parse level and text.
                int level = (int)headingElem.Attribute("level");
                string headingText = headingElem.Value.Trim();

                // ----- Add a new page for the heading -----
                Page contentPage = pdfDoc.Pages.Add();

                // Visible heading on the page.
                TextFragment tf = new TextFragment(headingText);
                tf.Position = new Position(50, contentPage.PageInfo.Height - 100);
                tf.TextState.FontSize = 24;
                tf.TextState.FontStyle = FontStyles.Bold;
                contentPage.Paragraphs.Add(tf);

                // Logical structure: HeaderElement with appropriate level.
                HeaderElement headerStruct = tagged.CreateHeaderElement(level);
                headerStruct.SetText(headingText);
                root.AppendChild(headerStruct);

                // ----- Add entry to TOC -----
                // Visible TOC entry.
                string tocLine = $"{headingText} ................................ {pageNumber}";
                TextFragment tocEntry = new TextFragment(tocLine);
                tocEntry.Position = new Position(50, yPos);
                tocEntry.TextState.FontSize = 12;
                tocPage.Paragraphs.Add(tocEntry);

                // Structure entry for TOC.
                ParagraphElement tocItemStruct = tagged.CreateParagraphElement();
                tocItemStruct.SetText(tocLine);
                tocStruct.AppendChild(tocItemStruct);

                // Link annotation from TOC entry to the heading page.
                // Rectangle coordinates are (llx, lly, urx, ury).
                Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(50, yPos - 5, 500, yPos + 10);
                // Use GoToAction directly with the target page (no Destination class).
                GoToAction goTo = new GoToAction(contentPage);
                LinkAnnotation link = new LinkAnnotation(tocPage, linkRect) { Action = goTo };
                tocPage.Annotations.Add(link);

                // Update positions.
                yPos -= lineHeight;
                pageNumber++;
            }

            // Save the PDF.
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with TOC saved to '{outputPdf}'.");
    }
}
