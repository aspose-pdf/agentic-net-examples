using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for input XML and output PDF
        const string xmlPath   = "input.xml";
        const string pdfPath   = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML document
        XDocument xDoc = XDocument.Load(xmlPath);

        // Assume the HTML snippet is stored in a CDATA section inside <HtmlSnippet> element
        // Example: <HtmlSnippet><![CDATA[<html><head><style>p{color:red;}</style></head><body><p>Hello</p></body></html>]]></HtmlSnippet>
        XElement htmlElement = xDoc.Root.Element("HtmlSnippet");
        if (htmlElement == null || string.IsNullOrWhiteSpace(htmlElement.Value))
        {
            Console.Error.WriteLine("No <HtmlSnippet> element with CDATA found in the XML.");
            return;
        }

        string htmlContent = htmlElement.Value; // CDATA content (CSS‑styled HTML)

        // Create a new PDF document and add a page
        using (Document pdfDoc = new Document())
        {
            Page page = pdfDoc.Pages.Add();

            // Create an HtmlFragment from the extracted HTML string
            HtmlFragment htmlFragment = new HtmlFragment(htmlContent);

            // Add the HtmlFragment to the page's paragraph collection
            page.Paragraphs.Add(htmlFragment);

            // Save the PDF
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated successfully: {pdfPath}");
    }
}