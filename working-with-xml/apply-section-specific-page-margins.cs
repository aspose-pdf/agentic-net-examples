using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string marginsXmlPath = "margins.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(marginsXmlPath))
        {
            Console.Error.WriteLine($"Margins definition XML not found: {marginsXmlPath}");
            return;
        }

        // Load the source PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Load XML that defines margin settings per section
            XDocument xml = XDocument.Load(marginsXmlPath);

            // Example XML structure:
            // <Margins>
            //   <Section StartPage="1" EndPage="3">
            //     <Top>20</Top>
            //     <Bottom>20</Bottom>
            //     <Left>30</Left>
            //     <Right>30</Right>
            //   </Section>
            //   <Section StartPage="4" EndPage="6">
            //     <Top>10</Top>
            //     <Bottom>10</Bottom>
            //     <Left>15</Left>
            //     <Right>15</Right>
            //   </Section>
            // </Margins>

            foreach (XElement section in xml.Root.Elements("Section"))
            {
                // Parse page range
                int startPage = (int)section.Attribute("StartPage");
                int endPage = (int)section.Attribute("EndPage");

                // Parse margin values (in points)
                int top = (int)section.Element("Top");
                int bottom = (int)section.Element("Bottom");
                int left = (int)section.Element("Left");
                int right = (int)section.Element("Right");

                // Create MarginInfo with the required side values
                MarginInfo marginInfo = new MarginInfo
                {
                    Top = top,
                    Bottom = bottom,
                    Left = left,
                    Right = right
                };

                // Apply the margin to each page in the defined range
                for (int pageNum = startPage; pageNum <= endPage && pageNum <= pdfDoc.Pages.Count; pageNum++)
                {
                    pdfDoc.Pages[pageNum].PageInfo.Margin = marginInfo;
                }
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with section-specific margins to '{outputPdfPath}'.");
    }
}
