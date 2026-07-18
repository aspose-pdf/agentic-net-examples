using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and XML file that defines margin sections
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string marginXmlPath = "margins.xml";

        // Verify that required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(marginXmlPath))
        {
            Console.Error.WriteLine($"Margin definition XML not found: {marginXmlPath}");
            return;
        }

        // Load margin definitions from XML.
        // Expected XML format:
        // <Margins>
        //   <Section Name="Header" StartPage="1" EndPage="2" Left="10" Right="10" Top="20" Bottom="5" />
        //   <Section Name="Body"   StartPage="3" EndPage="10" Left="15" Right="15" Top="25" Bottom="15" />
        //   ...
        // </Margins>
        XDocument marginDoc = XDocument.Load(marginXmlPath);
        var sections = marginDoc.Root.Elements("Section");

        // Open the PDF document inside a using block for proper disposal.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing).
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // Find the first section whose page range includes the current page.
                foreach (var sec in sections)
                {
                    int startPage = (int)sec.Attribute("StartPage");
                    int endPage   = (int)sec.Attribute("EndPage");

                    if (pageIndex >= startPage && pageIndex <= endPage)
                    {
                        // Extract margin values (they are in points).
                        double left   = (double)sec.Attribute("Left");
                        double right  = (double)sec.Attribute("Right");
                        double top    = (double)sec.Attribute("Top");
                        double bottom = (double)sec.Attribute("Bottom");

                        // Apply the margins to the page via PageInfo.Margin.
                        page.PageInfo.Margin = new MarginInfo(left, bottom, right, top);
                        break; // Section found; move to next page.
                    }
                }
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with custom margins to '{outputPdfPath}'.");
    }
}