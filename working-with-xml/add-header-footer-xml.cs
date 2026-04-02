using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace AddHeaderFooterFromXmlExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF with three pages
            using (Document document = new Document())
            {
                // Add three blank pages
                document.Pages.Add();
                document.Pages.Add();
                document.Pages.Add();

                // XML template defining header and footer content
                string xmlTemplate = @"<HeaderFooterTemplate>
                                            <Header>
                                                <Paragraph>Sample Header - Company XYZ</Paragraph>
                                            </Header>
                                            <Footer>
                                                <Paragraph>Page {pageNumber}</Paragraph>
                                            </Footer>
                                      </HeaderFooterTemplate>";

                // Load the XML
                XDocument xDoc = XDocument.Parse(xmlTemplate);
                XElement headerElement = xDoc.Root.Element("Header");
                XElement footerElement = xDoc.Root.Element("Footer");

                // Iterate through each page and assign header/footer
                for (int i = 1; i <= document.Pages.Count; i++)
                {
                    Page page = document.Pages[i];

                    // Create and assign header
                    HeaderFooter header = new HeaderFooter();
                    if (headerElement != null)
                    {
                        TextFragment headerFragment = new TextFragment(headerElement.Element("Paragraph").Value);
                        header.Paragraphs.Add(headerFragment);
                    }
                    page.Header = header;

                    // Create and assign footer with page number placeholder replacement
                    HeaderFooter footer = new HeaderFooter();
                    if (footerElement != null)
                    {
                        string footerText = footerElement.Element("Paragraph").Value;
                        footerText = footerText.Replace("{pageNumber}", i.ToString());
                        TextFragment footerFragment = new TextFragment(footerText);
                        footer.Paragraphs.Add(footerFragment);
                    }
                    page.Footer = footer;
                }

                // Save the resulting PDF
                document.Save("output.pdf");
            }
        }
    }
}
