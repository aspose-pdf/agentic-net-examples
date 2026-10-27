using System;
using System.IO;
using System.Linq;
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
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document actually contains a form with at least one field.
            if (doc.Form == null || doc.Form.Fields == null || !doc.Form.Fields.Any())
            {
                Console.WriteLine("No form fields found in the PDF.");
                return;
            }

            // Create a FileStream for the XML output and write a custom XML representation of the form fields.
            using (FileStream fs = new FileStream(outputXml, FileMode.Create, FileAccess.Write))
            using (XmlWriter writer = XmlWriter.Create(fs, new XmlWriterSettings { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("FormFields");

                foreach (var field in doc.Form.Fields)
                {
                    writer.WriteStartElement("Field");
                    // Use the field's Name if available, otherwise fall back to FullName.
                    string fieldName = field.Name ?? field.FullName ?? string.Empty;
                    writer.WriteAttributeString("Name", fieldName);
                    writer.WriteAttributeString("Type", field.GetType().Name);

                    // Write the field's value as element text (empty string if null).
                    string value = field.Value?.ToString() ?? string.Empty;
                    writer.WriteString(value);

                    writer.WriteEndElement(); // </Field>
                }

                writer.WriteEndElement(); // </FormFields>
                writer.WriteEndDocument();
            }
        }

        Console.WriteLine($"Form fields exported to XML: {outputXml}");
    }
}
