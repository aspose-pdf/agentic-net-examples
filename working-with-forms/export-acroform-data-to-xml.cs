using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputXml = "formdata.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdf))
            {
                // Prepare an XML writer for the output file
                using (XmlWriter writer = XmlWriter.Create(outputXml, new XmlWriterSettings { Indent = true }))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("FormData");

                    // Iterate over all form fields in the document
                    foreach (Field field in doc.Form.Fields)
                    {
                        // Retrieve the field name (PartialName) and its current value
                        string fieldName  = field.PartialName ?? string.Empty;
                        string fieldValue = field.Value?.ToString() ?? string.Empty;

                        writer.WriteStartElement("Field");
                        writer.WriteAttributeString("Name", fieldName);
                        writer.WriteString(fieldValue);
                        writer.WriteEndElement(); // Field
                    }

                    writer.WriteEndElement(); // FormData
                    writer.WriteEndDocument();
                }

                Console.WriteLine($"Form data exported to XML: {outputXml}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}