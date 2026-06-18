using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputXmlPath  = "input.xml";   // XML containing CDATA sections with HTML
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputXmlPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputXmlPath}");
            return;
        }

        // Load the XML document (no special load options required for plain XML)
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(inputXmlPath);

        // Create a new PDF document and ensure deterministic disposal
        using (Document pdfDoc = new Document())
        {
            // Add a single page – additional pages can be added as needed
            Page page = pdfDoc.Pages.Add();

            // Iterate through all CDATA sections in the XML
            foreach (XmlNode node in xmlDoc.SelectNodes("//*"))
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    if (child.NodeType == XmlNodeType.CDATA)
                    {
                        // The CDATA value contains an HTML fragment
                        string htmlFragment = child.Value;

                        // Create an Aspose.Pdf.HtmlFragment from the HTML string
                        HtmlFragment fragment = new HtmlFragment(htmlFragment);

                        // Add the fragment to the page's paragraph collection
                        page.Paragraphs.Add(fragment);
                    }
                }
            }

            // Save the result as a PDF file
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF generated successfully: {outputPdfPath}");
    }
}