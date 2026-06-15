using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // XML string containing form data (XFA format)
        string xmlData = @"<?xml version='1.0' encoding='UTF-8'?>
<xfa:datasets xmlns:xfa='http://www.xfa.org/schema/xfa-data/1.0/'>
    <xfa:data>
        <field1>Value1</field1>
        <field2>Value2</field2>
    </xfa:data>
</xfa:datasets>";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Parse the XML string into an XmlDocument
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlData);

            // Import the XML data into the PDF form (XFA)
            doc.Form.AssignXfa(xmlDoc);

            // Save the updated PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Form data imported and saved to '{outputPdf}'.");
    }
}