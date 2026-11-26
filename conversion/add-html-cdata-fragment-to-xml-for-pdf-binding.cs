using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the XML data, PDF template, and output PDF
        const string xmlPath = "data.xml";
        const string pdfTemplatePath = "template.pdf";
        const string outputPdfPath = "result.pdf";

        // Ensure the PDF template exists; create a minimal one if it does not.
        if (!File.Exists(pdfTemplatePath))
        {
            using (var doc = new Document())
            {
                // Add a single blank page.
                doc.Pages.Add();
                doc.Save(pdfTemplatePath);
            }
        }

        // Load existing XML or create a new one with a root element
        XmlDocument xmlDoc = new XmlDocument();
        if (File.Exists(xmlPath))
        {
            xmlDoc.Load(xmlPath);
        }
        else
        {
            XmlElement root = xmlDoc.CreateElement("Root");
            xmlDoc.AppendChild(root);
        }

        // Create an HtmlFragment element that contains CDATA with HTML markup
        XmlElement htmlFragment = xmlDoc.CreateElement("HtmlFragment");
        XmlCDataSection cdata = xmlDoc.CreateCDataSection("<p style=\"color:red;\">Hello <b>World</b></p>");
        htmlFragment.AppendChild(cdata);

        // Append the new HtmlFragment element to the document's root
        xmlDoc.DocumentElement?.AppendChild(htmlFragment);

        // Save the modified XML to a temporary file for binding
        string tempXmlPath = Path.Combine(Path.GetTempPath(), "tempData.xml");
        xmlDoc.Save(tempXmlPath);

        // Load the PDF template and bind the XML data
        using (Document pdfDoc = new Document(pdfTemplatePath))
        {
            pdfDoc.BindXml(tempXmlPath); // Bind the XML before saving
            pdfDoc.Save(outputPdfPath);  // Save the resulting PDF
        }

        // Remove the temporary XML file
        if (File.Exists(tempXmlPath))
        {
            File.Delete(tempXmlPath);
        }

        Console.WriteLine($"PDF generated at '{outputPdfPath}'.");
    }
}
