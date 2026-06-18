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
        const string inputPdf  = "input.pdf";
        const string outputXml = "selected_fields.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // List of fully qualified field names to export
            var fieldsToExport = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "Customer.Name",
                "Customer.Email",
                "Order.Total"
            };

            // Prepare XML writer
            using (var writer = XmlWriter.Create(outputXml, new XmlWriterSettings { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("FormData");

                // Iterate over all form fields and write only the selected ones
                foreach (Field field in pdfDoc.Form.Fields)
                {
                    // FullName provides the fully qualified name of the field
                    if (fieldsToExport.Contains(field.FullName))
                    {
                        writer.WriteStartElement("Field");
                        writer.WriteAttributeString("Name", field.FullName);
                        writer.WriteString(field.Value?.ToString() ?? string.Empty);
                        writer.WriteEndElement(); // Field
                    }
                }

                writer.WriteEndElement(); // FormData
                writer.WriteEndDocument();
            }

            Console.WriteLine($"Selected fields exported to '{outputXml}'.");
        }
    }
}