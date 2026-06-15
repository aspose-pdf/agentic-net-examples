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
        const string outputXml = "form_fields.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Create a FileStream for the XML output
            using (FileStream fs = new FileStream(outputXml, FileMode.Create, FileAccess.Write))
            {
                // Set up an XmlWriter with indentation for readability
                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = "  "
                };

                using (XmlWriter writer = XmlWriter.Create(fs, settings))
                {
                    // Begin the XML document
                    writer.WriteStartDocument();
                    writer.WriteStartElement("FormFields");

                    // Iterate over all form fields and write each as an XML element
                    foreach (Field field in doc.Form.Fields)
                    {
                        writer.WriteStartElement("Field");
                        writer.WriteAttributeString("Name", field.Name ?? string.Empty);
                        // Some field types may not have a direct Value property; use ToString as fallback
                        string value = field.Value?.ToString() ?? string.Empty;
                        writer.WriteString(value);
                        writer.WriteEndElement(); // </Field>
                    }

                    writer.WriteEndElement(); // </FormFields>
                    writer.WriteEndDocument();
                }
            }
        }

        Console.WriteLine($"Form fields have been exported to XML file: {outputXml}");
    }
}