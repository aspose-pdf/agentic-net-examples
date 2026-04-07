using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";   // PDF with form fields
        const string xmlPath      = "data.xml";      // XML containing multiple records
        const string outputDir    = "Output";        // Folder for generated PDFs

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML data not found: {xmlPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the whole XML document
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlPath);

        // Assume each record is represented by a <Record> element
        XmlNodeList records = xmlDoc.SelectNodes("//Record");
        if (records == null || records.Count == 0)
        {
            Console.WriteLine("No <Record> elements found in the XML file.");
            return;
        }

        int recordIndex = 1;
        foreach (XmlNode recordNode in records)
        {
            // Open a fresh copy of the PDF template for each record
            using (Document pdfDoc = new Document(templatePath))
            {
                // Build a minimal XmlDocument that contains the current record.
                // AssignXfa expects an XmlDocument; we wrap the record node in a root element.
                XmlDocument recordXml = new XmlDocument();
                XmlElement root = recordXml.CreateElement("xfdf"); // arbitrary root name
                XmlNode importedNode = recordXml.ImportNode(recordNode, true);
                root.AppendChild(importedNode);
                recordXml.AppendChild(root);

                // Populate the form fields with the XML data
                pdfDoc.Form.AssignXfa(recordXml);

                // Save the filled PDF
                string outputPath = Path.Combine(outputDir, $"filled_{recordIndex}.pdf");
                pdfDoc.Save(outputPath);
                Console.WriteLine($"Saved filled PDF: {outputPath}");
            }

            recordIndex++;
        }
    }
}