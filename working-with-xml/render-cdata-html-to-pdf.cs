using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for HtmlFragment base classes

class Program
{
    static void Main()
    {
        const string inputXmlPath  = "input.xml";   // XML containing CDATA with HTML
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputXmlPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputXmlPath}");
            return;
        }

        // Load the XML document (no special load options needed for plain XML)
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(inputXmlPath);

        // Create an empty PDF document and add a single page
        using (Document pdfDoc = new Document())
        {
            pdfDoc.Pages.Add();

            // Iterate over all CDATA sections in the XML
            XmlNodeList cdataNodes = xmlDoc.SelectNodes("//text()"); // selects all text nodes
            foreach (XmlNode node in cdataNodes)
            {
                if (node.NodeType == XmlNodeType.CDATA)
                {
                    string htmlContent = node.Value;

                    // Create an HtmlFragment from the CDATA HTML string
                    HtmlFragment htmlFragment = new HtmlFragment(htmlContent);

                    // Optional: customize loading of the HTML fragment (e.g., base path)
                    // htmlFragment.HtmlLoadOptions = new HtmlLoadOptions(Path.GetDirectoryName(inputXmlPath));

                    // Add the fragment to the first page's paragraph collection
                    pdfDoc.Pages[1].Paragraphs.Add(htmlFragment);
                }
            }

            // Save the resulting PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF generated successfully: {outputPdfPath}");
    }
}