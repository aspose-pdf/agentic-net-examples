using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXml = "selected_fields.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Define the field names that should be exported
            var fieldsToExport = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "FirstName",
                "LastName",
                "Email"
            };

            // Create an XML writer that writes directly to the target file
            var settings = new XmlWriterSettings { Indent = true, Encoding = System.Text.Encoding.UTF8 };
            using (XmlWriter writer = XmlWriter.Create(outputXml, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("FormData");

                // Iterate over all form fields and write only the selected ones
                foreach (Field field in doc.Form.Fields)
                {
                    if (fieldsToExport.Contains(field.PartialName))
                    {
                        writer.WriteStartElement("Field");
                        writer.WriteAttributeString("Name", field.PartialName);
                        writer.WriteString(field.Value?.ToString() ?? string.Empty);
                        writer.WriteEndElement(); // Field
                    }
                }

                writer.WriteEndElement(); // FormData
                writer.WriteEndDocument();
            }
        }

        Console.WriteLine($"Selected form fields exported to '{outputXml}'.");
    }
}
