using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // required for HtmlFragment

class RenderHtmlCDataToPdf
{
    static void Main()
    {
        // Input XML file that contains HTML fragments inside CDATA sections
        const string xmlPath   = "input.xml";
        // Output PDF file
        const string pdfPath   = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Create an empty PDF document
        using (Document pdfDoc = new Document())
        {
            // Add a single page – additional pages can be added as needed
            Page page = pdfDoc.Pages.Add();

            // Load the XML document
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            // Select all CDATA sections (they appear as XmlCDataSection nodes)
            XmlNodeList cdataNodes = xmlDoc.SelectNodes("//text()"); // selects all text nodes, including CDATA

            foreach (XmlNode node in cdataNodes)
            {
                if (node is XmlCDataSection cdata && !string.IsNullOrWhiteSpace(cdata.Data))
                {
                    // The CDATA content is expected to be an HTML fragment
                    string htmlFragment = cdata.Data.Trim();

                    // Create an HtmlFragment instance – it will render the HTML when the PDF is saved
                    HtmlFragment fragment = new HtmlFragment(htmlFragment);

                    // Optional: configure HtmlLoadOptions if the fragment references external resources
                    // fragment.HtmlLoadOptions = new HtmlLoadOptions(); // use defaults or set BasePath, etc.

                    // Add the fragment to the page's paragraph collection
                    page.Paragraphs.Add(fragment);
                }
            }

            // Save the resulting PDF
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated successfully: {pdfPath}");
    }
}