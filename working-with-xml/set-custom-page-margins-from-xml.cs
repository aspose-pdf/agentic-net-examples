using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string marginsXml = "margins.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(marginsXml))
        {
            Console.Error.WriteLine($"XML not found: {marginsXml}");
            return;
        }

        // Load margin definitions from XML.
        // Expected format:
        // <Sections>
        //   <Section pageStart="1" pageEnd="3">
        //     <Margins top="10" left="15" right="15" bottom="10"/>
        //   </Section>
        //   ...
        // </Sections>
        XDocument xmlDoc = XDocument.Load(marginsXml);
        var sections = xmlDoc.Root.Elements("Section");

        // Open the PDF document.
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Iterate over each section defined in the XML.
            foreach (var sec in sections)
            {
                int startPage = (int)sec.Attribute("pageStart");
                int endPage   = (int)sec.Attribute("pageEnd");

                // Read margin values (in points). If an attribute is missing, default to 0.
                var marginsElem = sec.Element("Margins");
                double top    = (double?)marginsElem.Attribute("top")    ?? 0;
                double left   = (double?)marginsElem.Attribute("left")   ?? 0;
                double right  = (double?)marginsElem.Attribute("right")  ?? 0;
                double bottom = (double?)marginsElem.Attribute("bottom") ?? 0;

                // Apply margins to each page in the range.
                for (int pageNum = startPage; pageNum <= endPage && pageNum <= pdfDoc.Pages.Count; pageNum++)
                {
                    Page page = pdfDoc.Pages[pageNum];

                    // Ensure a PageInfo instance exists (it is always non‑null, but guard just in case).
                    if (page.PageInfo == null)
                        page.PageInfo = new PageInfo();

                    // Create a MarginInfo and set the four sides directly.
                    MarginInfo marginInfo = new MarginInfo
                    {
                        Top    = top,
                        Bottom = bottom,
                        Left   = left,
                        Right  = right
                    };

                    // Assign the margin to the page.
                    page.PageInfo.Margin = marginInfo;
                }
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with custom margins to '{outputPdf}'.");
    }
}
