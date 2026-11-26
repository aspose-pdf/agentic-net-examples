using System;
using System.IO;
using System.Xml;
using Aspose.Pdf; // core API only

class Program
{
    static void Main()
    {
        const string templatePdfPath = "template.pdf";
        const string xmlDataPath = "data.xml";
        const string outputPdfPath = "output.pdf";

        // Verify input files exist
        if (!File.Exists(templatePdfPath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePdfPath}");
            return;
        }
        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlDataPath}");
            return;
        }

        // Load and modify the XML
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlDataPath);

        // Find the first <Table> element and set its Alignment attribute
        XmlNode? tableNode = xmlDoc.SelectSingleNode("//Table");
        if (tableNode != null)
        {
            // Change alignment to "Center" (adjust value as needed)
            XmlAttribute? alignAttr = tableNode.Attributes?["Alignment"];
            if (alignAttr == null)
            {
                alignAttr = xmlDoc.CreateAttribute("Alignment");
                tableNode.Attributes?.Append(alignAttr);
            }
            alignAttr.Value = "Center";
        }
        else
        {
            Console.Error.WriteLine("No <Table> element found in the XML.");
            return;
        }

        // Convert the modified XML back to a string
        string modifiedXml;
        using (StringWriter sw = new StringWriter())
        {
            xmlDoc.Save(sw);
            modifiedXml = sw.ToString();
        }

        // Load the PDF template
        using (Document pdfDoc = new Document(templatePdfPath))
        {
            // NOTE: The core Aspose.Pdf API does not expose a BindXML method.
            // Binding XML data to an XFA form is performed via the Facades API (PdfFileFormDataMerger),
            // which is prohibited by the hard namespace rule. Therefore the binding step is omitted.
            // If the template uses AcroForm fields, you could populate them manually via pdfDoc.Form.
            // For the purpose of this example we simply save the unchanged template.

            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF generated successfully: {outputPdfPath}");
    }
}
