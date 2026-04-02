using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

namespace AsposePdfMarginExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // -------------------------------------------------------------------
            // 1. Create a sample PDF with four blank pages.
            // -------------------------------------------------------------------
            string inputPath = Path.Combine(Directory.GetCurrentDirectory(), "input.pdf");
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "output.pdf");

            using (Document doc = new Document())
            {
                // Add four blank pages
                doc.Pages.Add();
                doc.Pages.Add();
                doc.Pages.Add();
                doc.Pages.Add();

                // Save the sample PDF – this guarantees the file exists on disk
                doc.Save(inputPath);
            }

            // -------------------------------------------------------------------
            // 2. Define XML with margin settings for odd and even pages.
            // -------------------------------------------------------------------
            string xmlContent = @"<Margins>
                                    <Section name='Odd'>
                                        <Top>50</Top>
                                        <Bottom>50</Bottom>
                                        <Left>30</Left>
                                        <Right>30</Right>
                                    </Section>
                                    <Section name='Even'>
                                        <Top>20</Top>
                                        <Bottom>20</Bottom>
                                        <Left>10</Left>
                                        <Right>10</Right>
                                    </Section>
                                </Margins>";

            XmlDocument marginXml = new XmlDocument();
            marginXml.LoadXml(xmlContent);

            // Extract margin values for odd pages
            XmlNode oddNode = marginXml.SelectSingleNode("/Margins/Section[@name='Odd']");
            double oddTop = double.Parse(oddNode["Top"].InnerText);
            double oddBottom = double.Parse(oddNode["Bottom"].InnerText);
            double oddLeft = double.Parse(oddNode["Left"].InnerText);
            double oddRight = double.Parse(oddNode["Right"].InnerText);

            // Extract margin values for even pages
            XmlNode evenNode = marginXml.SelectSingleNode("/Margins/Section[@name='Even']");
            double evenTop = double.Parse(evenNode["Top"].InnerText);
            double evenBottom = double.Parse(evenNode["Bottom"].InnerText);
            double evenLeft = double.Parse(evenNode["Left"].InnerText);
            double evenRight = double.Parse(evenNode["Right"].InnerText);

            // -------------------------------------------------------------------
            // 3. Open the PDF that we just created and apply margins conditionally.
            // -------------------------------------------------------------------
            using (Document doc = new Document(inputPath))
            {
                int pageCount = doc.Pages.Count;
                for (int i = 1; i <= pageCount; i++)
                {
                    // Choose the appropriate margin set based on odd/even page index
                    if (i % 2 == 1) // odd page
                    {
                        doc.Pages[i].PageInfo.AnyMargin = new MarginInfo(oddLeft, oddBottom, oddRight, oddTop);
                    }
                    else // even page
                    {
                        doc.Pages[i].PageInfo.AnyMargin = new MarginInfo(evenLeft, evenBottom, evenRight, evenTop);
                    }
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }
        }
    }
}
