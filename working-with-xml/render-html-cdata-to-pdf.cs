using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string pdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML document (no special load options required for plain XML)
        XDocument xDoc = XDocument.Load(xmlPath);

        // Create a new empty PDF document
        using (Document pdfDoc = new Document())
        {
            // Iterate over all CDATA sections in the XML
            foreach (XCData cdata in xDoc.DescendantNodes().OfType<XCData>())
            {
                // Each CDATA is assumed to contain an HTML fragment
                string htmlContent = cdata.Value;

                // Add a new page for this fragment
                Page page = pdfDoc.Pages.Add();

                // Create an HtmlFragment from the HTML string
                Aspose.Pdf.HtmlFragment htmlFragment = new Aspose.Pdf.HtmlFragment(htmlContent);

                // Add the fragment to the page's paragraph collection
                page.Paragraphs.Add(htmlFragment);
            }

            // Save the resulting PDF
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated successfully: {pdfPath}");
    }
}