using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace AddHeaderFooterXmlExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // -------------------------------------------------------------------
            // Step 1: Create a sample PDF (self‑contained example)
            // -------------------------------------------------------------------
            using (Document sampleDoc = new Document())
            {
                // Add three pages – evaluation mode allows up to four pages
                int pageCount = 3;
                for (int i = 0; i < pageCount; i++)
                {
                    sampleDoc.Pages.Add();
                }
                // Save the temporary PDF that will be used as input
                sampleDoc.Save("input.pdf");
            }

            // -------------------------------------------------------------------
            // Step 2: Load the PDF and prepare XML templates for header/footer
            // -------------------------------------------------------------------
            using (Document doc = new Document("input.pdf"))
            {
                // XML template for the header – uses pagination placeholders $p (current page) and $P (total pages)
                string headerXml = "<Header><Paragraph>Sample Header – Page $p of $P</Paragraph></Header>";
                // XML template for the footer – uses date placeholder $d (current date)
                string footerXml = "<Footer><Paragraph>Sample Footer – Generated on $d</Paragraph></Footer>";

                // Parse header XML
                XmlDocument headerDoc = new XmlDocument();
                headerDoc.LoadXml(headerXml);
                XmlNode headerParagraphNode = headerDoc.SelectSingleNode("/Header/Paragraph");
                HeaderFooter header = new HeaderFooter();
                if (headerParagraphNode != null)
                {
                    TextFragment headerFragment = new TextFragment(headerParagraphNode.InnerText);
                    header.Paragraphs.Add(headerFragment);
                }

                // Parse footer XML
                XmlDocument footerDoc = new XmlDocument();
                footerDoc.LoadXml(footerXml);
                XmlNode footerParagraphNode = footerDoc.SelectSingleNode("/Footer/Paragraph");
                HeaderFooter footer = new HeaderFooter();
                if (footerParagraphNode != null)
                {
                    TextFragment footerFragment = new TextFragment(footerParagraphNode.InnerText);
                    footer.Paragraphs.Add(footerFragment);
                }

                // Assign the header and footer to every page in the document
                foreach (Page page in doc.Pages)
                {
                    page.Header = header;
                    page.Footer = footer;
                }

                // Update pagination placeholders ($p, $P, $d) in all headers/footers
                doc.Pages.UpdatePagination();

                // Save the final PDF with header and footer applied
                doc.Save("output.pdf");
            }
        }
    }
}
