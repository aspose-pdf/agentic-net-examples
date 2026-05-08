using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfTemplatePath = "template.pdf";   // PDF with form fields
        const string xmlDataPath     = "data.xml";       // XML containing multiple records
        const string outputFolder    = "FilledForms";

        // Verify input files exist
        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"Template PDF not found: {pdfTemplatePath}");
            return;
        }
        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlDataPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the XML that holds all records
        XmlDocument masterXml = new XmlDocument();
        masterXml.Load(xmlDataPath);

        // Select each record node – adjust XPath to match your XML structure
        XmlNodeList recordNodes = masterXml.SelectNodes("//Record");
        if (recordNodes == null || recordNodes.Count == 0)
        {
            Console.Error.WriteLine("No <Record> elements found in the XML.");
            return;
        }

        int counter = 1;
        foreach (XmlNode recordNode in recordNodes)
        {
            // Build a minimal XFA XML document containing only the current record.
            // The exact structure depends on the PDF's XFA template; this example
            // wraps the record inside a root element named "xfa".
            XmlDocument recordXml = new XmlDocument();
            XmlElement root = recordXml.CreateElement("xfa");
            recordXml.AppendChild(root);
            XmlNode importedRecord = recordXml.ImportNode(recordNode, true);
            root.AppendChild(importedRecord);

            // Load the PDF template, assign the XFA data, and save a new file.
            using (Document pdfDoc = new Document(pdfTemplatePath))
            {
                // Populate the form with the XML data for this record.
                pdfDoc.Form.AssignXfa(recordXml);

                // Save the filled PDF with a unique name.
                string outputPath = Path.Combine(outputFolder, $"filled_{counter}.pdf");
                pdfDoc.Save(outputPath);
                Console.WriteLine($"Saved filled form: {outputPath}");
            }

            counter++;
        }
    }
}