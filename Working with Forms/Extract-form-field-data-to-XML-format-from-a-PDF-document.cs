using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputXmlPath = "form_fields.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Export all annotations (including form field annotations) to XFDF in memory
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                doc.ExportAnnotationsToXfdf(xfdfStream);
                xfdfStream.Position = 0; // reset stream for reading

                // Parse the XFDF stream with XfdfReader.GetElements to obtain field name/value pairs
                Dictionary<string, string> fieldData;
                using (XmlReader xmlReader = XmlReader.Create(xfdfStream))
                {
                    fieldData = XfdfReader.GetElements(xmlReader);
                }

                // Build an XML document containing the extracted form field data
                XmlDocument xmlDoc = new XmlDocument();
                XmlElement root = xmlDoc.CreateElement("FormFields");
                xmlDoc.AppendChild(root);

                foreach (KeyValuePair<string, string> kvp in fieldData)
                {
                    XmlElement fieldElem = xmlDoc.CreateElement("Field");
                    fieldElem.SetAttribute("name", kvp.Key);
                    fieldElem.InnerText = kvp.Value ?? string.Empty;
                    root.AppendChild(fieldElem);
                }

                // Save the XML document to the specified output path
                xmlDoc.Save(outputXmlPath);
                Console.WriteLine($"Form field data extracted to XML: {outputXmlPath}");
            }
        }
    }
}