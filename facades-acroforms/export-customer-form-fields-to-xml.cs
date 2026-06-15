using System;
using System.IO;
using System.Xml;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXml = "customer_fields.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the Form facade with the source PDF
        using (Form form = new Form(inputPdf))
        {
            // Create an XML writer for the output file
            using (FileStream fs = new FileStream(outputXml, FileMode.Create, FileAccess.Write))
            using (XmlWriter writer = XmlWriter.Create(fs, new XmlWriterSettings { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Fields");

                // Iterate over all field names and export only those starting with "Customer"
                foreach (string fieldName in form.FieldNames)
                {
                    if (fieldName.StartsWith("Customer", StringComparison.OrdinalIgnoreCase))
                    {
                        // Retrieve the field's value as a string
                        string value = form.GetField(fieldName);

                        writer.WriteStartElement("Field");
                        writer.WriteAttributeString("name", fieldName);
                        writer.WriteString(value ?? string.Empty);
                        writer.WriteEndElement(); // </Field>
                    }
                }

                writer.WriteEndElement(); // </Fields>
                writer.WriteEndDocument();
            }
        }

        Console.WriteLine($"Exported customer fields to '{outputXml}'.");
    }
}