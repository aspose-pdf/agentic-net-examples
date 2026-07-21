using System;
using System.IO;
using System.Xml;
using Aspose.Pdf; // Core PDF API – XmlSaveOptions is now in this namespace

class ExportFormData
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF containing form fields
        const string outputXmlPath = "formdata.xml";      // target XML file
        const string customRoot = "ExternalSystemForm"; // required root element name

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Export the PDF structure (including form data) to XML in memory
            XmlSaveOptions xmlOpts = new XmlSaveOptions(); // default options, now in Aspose.Pdf namespace
            using (MemoryStream xmlStream = new MemoryStream())
            {
                pdfDoc.Save(xmlStream, xmlOpts);   // write XML to the stream
                xmlStream.Position = 0;            // reset for reading

                // Load the generated XML into an XmlDocument for manipulation
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlStream);

                // Rename the root element to match the external system's requirement
                XmlElement oldRoot = xmlDoc.DocumentElement;
                XmlElement newRoot = xmlDoc.CreateElement(customRoot);

                // Preserve any attributes from the original root
                if (oldRoot.HasAttributes)
                {
                    foreach (XmlAttribute attr in oldRoot.Attributes)
                        newRoot.Attributes.Append((XmlAttribute)attr.CloneNode(true));
                }

                // Move all child nodes from the old root to the new root
                while (oldRoot.HasChildNodes)
                {
                    XmlNode child = oldRoot.FirstChild;
                    oldRoot.RemoveChild(child);
                    newRoot.AppendChild(child);
                }

                // Replace the old root with the new one in the document
                xmlDoc.ReplaceChild(newRoot, oldRoot);

                // Save the modified XML to the desired file
                xmlDoc.Save(outputXmlPath);
                Console.WriteLine($"Form data exported to XML with root '{customRoot}' at '{outputXmlPath}'.");
            }
        }
    }
}
