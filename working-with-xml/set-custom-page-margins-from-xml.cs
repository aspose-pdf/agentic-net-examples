using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xmlPath = "margins.xml";
        const string outputPdf = "output_with_margins.pdf";

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

        // Load margin definitions from XML.
        // Expected XML format:
        // <Margins>
        //   <Page number="1">
        //     <Margin top="20" left="15" right="15" bottom="20"/>
        //   </Page>
        //   <Page number="2">
        //     <Margin top="10" left="10" right="10" bottom="10"/>
        //   </Page>
        //   ...
        // </Margins>
        var pageMargins = new Dictionary<int, MarginInfo>();

        try
        {
            XDocument xDoc = XDocument.Load(xmlPath);
            foreach (var pageElem in xDoc.Root.Elements("Page"))
            {
                int pageNumber = (int)pageElem.Attribute("number");
                var marginElem = pageElem.Element("Margin");
                if (marginElem == null) continue;

                // Values are expected in points. If they are in other units, convert accordingly.
                double top    = (double)marginElem.Attribute("top");
                double left   = (double)marginElem.Attribute("left");
                double right  = (double)marginElem.Attribute("right");
                double bottom = (double)marginElem.Attribute("bottom");

                var marginInfo = new MarginInfo
                {
                    Top    = top,
                    Left   = left,
                    Right  = right,
                    Bottom = bottom
                };

                pageMargins[pageNumber] = marginInfo;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to parse XML: {ex.Message}");
            return;
        }

        // Load the PDF, apply margins, and save.
        try
        {
            using (var pdfDoc = new Document(pdfPath))
            {
                // Iterate over all pages (1‑based indexing)
                for (int i = 1; i <= pdfDoc.Pages.Count; i++)
                {
                    if (pageMargins.TryGetValue(i, out var mi))
                    {
                        // Set the margin for the current page.
                        pdfDoc.Pages[i].PageInfo.Margin = mi;
                    }
                }

                // Save the modified PDF.
                pdfDoc.Save(outputPdf);
                Console.WriteLine($"PDF saved with custom margins to '{outputPdf}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"PDF processing error: {ex.Message}");
        }
    }
}
