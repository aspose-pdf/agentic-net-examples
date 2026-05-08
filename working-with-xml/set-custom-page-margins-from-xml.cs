using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf; // Core PDF API – MarginInfo is in this namespace

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";      // source PDF
        const string xmlPath   = "margins.xml";   // XML with per‑section margin definitions
        const string outputPdf = "output.pdf";    // result PDF

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

        // Load the XML that defines margins per section.
        // Expected format:
        // <Sections>
        //   <Section startPage="1" endPage="3">
        //     <Margins top="10" left="15" bottom="10" right="15"/>
        //   </Section>
        //   ...
        // </Sections>
        XDocument xmlDoc = XDocument.Load(xmlPath);

        // Load the PDF document.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Iterate over each section defined in the XML.
            foreach (XElement section in xmlDoc.Root?.Elements("Section") ?? Array.Empty<XElement>())
            {
                int startPage = (int)section.Attribute("startPage");
                int endPage   = (int)section.Attribute("endPage");

                XElement marginsElem = section.Element("Margins");
                if (marginsElem == null) continue; // skip if no margin info

                double top    = (double)marginsElem.Attribute("top");
                double left   = (double)marginsElem.Attribute("left");
                double bottom = (double)marginsElem.Attribute("bottom");
                double right  = (double)marginsElem.Attribute("right");

                // Build a MarginInfo instance with the required values.
                MarginInfo marginInfo = new MarginInfo
                {
                    Top    = top,
                    Bottom = bottom,
                    Left   = left,
                    Right  = right
                };

                // Apply the margin to every page in the range (Aspose.Pdf uses 1‑based indexing).
                for (int pageNum = startPage; pageNum <= endPage && pageNum <= pdfDoc.Pages.Count; pageNum++)
                {
                    pdfDoc.Pages[pageNum].PageInfo.Margin = marginInfo;
                }
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with custom margins to '{outputPdf}'.");
    }
}
