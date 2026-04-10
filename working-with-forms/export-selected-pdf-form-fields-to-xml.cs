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
        const string inputPdfPath = "input.pdf";
        const string outputXmlPath = "selected_fields.xml";

        // List of field names to export (full qualified names)
        var fieldsToExport = new List<string>
        {
            "FirstName",
            "LastName",
            "Email"
        };

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (var document = new Document(inputPdfPath))
        {
            // Ensure the document contains a form
            if (document.Form == null || document.Form.Count == 0)
            {
                Console.WriteLine("No form fields found in the PDF.");
                return;
            }

            // Create an XML document to hold the exported data
            var xmlDoc = new XmlDocument();
            XmlElement root = xmlDoc.CreateElement("FormData");
            xmlDoc.AppendChild(root);

            // Export only the specified fields
            foreach (string fieldName in fieldsToExport)
            {
                // The Form indexer returns a WidgetAnnotation; cast it to Field safely.
                Field field = document.Form[fieldName] as Field;
                if (field == null)
                {
                    Console.WriteLine($"Field '{fieldName}' not found or is not a form field; skipping.");
                    continue;
                }

                // Get the field value as a string (handles different field types)
                string value = field.Value?.ToString() ?? string.Empty;

                // Create an XML element for this field
                XmlElement fieldElement = xmlDoc.CreateElement("Field");
                fieldElement.SetAttribute("Name", fieldName);
                fieldElement.InnerText = value;
                root.AppendChild(fieldElement);
            }

            // Save the XML to the specified file
            using (var writer = new XmlTextWriter(outputXmlPath, System.Text.Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                xmlDoc.WriteContentTo(writer);
            }

            Console.WriteLine($"Selected form fields exported to '{outputXmlPath}'.");
        }
    }
}
