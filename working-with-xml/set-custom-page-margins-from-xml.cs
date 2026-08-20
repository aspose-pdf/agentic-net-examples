using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";      // source PDF
        const string xmlPath   = "margins.xml";    // XML with margin definitions per section
        const string outputPdf = "output.pdf";     // PDF with updated margins

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
        //   <Section StartPage="1" EndPage="3">
        //     <Top>10</Top>
        //     <Bottom>10</Bottom>
        //     <Left>20</Left>
        //     <Right>20</Right>
        //   </Section>
        //   ...
        // </Sections>
        XDocument xmlDoc = XDocument.Load(xmlPath);

        // Open the PDF inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Iterate over each <Section> element in the XML.
            foreach (XElement section in xmlDoc.Root.Elements("Section"))
            {
                // Parse page range.
                int startPage = (int)section.Attribute("StartPage");
                int endPage   = (int)section.Attribute("EndPage");

                // Parse margin values (assumed to be in points).
                int top    = (int)section.Element("Top");
                int bottom = (int)section.Element("Bottom");
                int left   = (int)section.Element("Left");
                int right  = (int)section.Element("Right");

                // Apply margins to each page in the range (Aspose.Pdf uses 1‑based indexing).
                for (int i = startPage; i <= endPage && i <= pdfDoc.Pages.Count; i++)
                {
                    Page page = pdfDoc.Pages[i];

                    // Set the margin directly via MarginInfo.
                    page.PageInfo.Margin = new MarginInfo
                    {
                        Top    = top,
                        Bottom = bottom,
                        Left   = left,
                        Right  = right
                    };
                }
            }

            // Save the modified PDF. No SaveOptions needed for PDF output.
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with custom margins to '{outputPdf}'.");
    }
}
