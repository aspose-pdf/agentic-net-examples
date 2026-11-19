using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string marginsXmlPath = "margins.xml";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(marginsXmlPath))
        {
            Console.Error.WriteLine($"Margins XML not found: {marginsXmlPath}");
            return;
        }

        // Load margin definitions from XML.
        // Expected format:
        // <Margins>
        //   <Section start="1" end="3" top="50" bottom="50" left="40" right="40"/>
        //   <Section start="4" end="6" top="30" bottom="30" left="20" right="20"/>
        //   ...
        // </Margins>
        XDocument xmlDoc = XDocument.Load(marginsXmlPath);
        var sections = xmlDoc.Root.Elements("Section");

        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate through all pages (1‑based indexing).
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                // Find the first section whose page range includes the current page.
                var matchingSection = FindSectionForPage(sections, i);
                if (matchingSection != null)
                {
                    // Parse margin values; default to 0 if missing.
                    double top    = (double?)matchingSection.Attribute("top")    ?? 0;
                    double bottom = (double?)matchingSection.Attribute("bottom") ?? 0;
                    double left   = (double?)matchingSection.Attribute("left")   ?? 0;
                    double right  = (double?)matchingSection.Attribute("right")  ?? 0;

                    // Apply margins to the page.
                    pdfDoc.Pages[i].PageInfo.Margin = new MarginInfo
                    {
                        Top    = top,
                        Bottom = bottom,
                        Left   = left,
                        Right  = right
                    };
                }
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with section‑specific margins to '{outputPdfPath}'.");
    }

    // Helper to locate the section element that covers a given page number.
    static XElement FindSectionForPage(IEnumerable<XElement> sections, int pageNumber)
    {
        foreach (var sec in sections)
        {
            int start = (int?)sec.Attribute("start") ?? 1;
            int end   = (int?)sec.Attribute("end")   ?? int.MaxValue;
            if (pageNumber >= start && pageNumber <= end)
                return sec;
        }
        return null;
    }
}