using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // Added for Form and Field types

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
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Access the form object which implements IEnumerable<Field>
            Form pdfForm = pdfDoc.Form;

            // Prepare an XmlWriter to create the output XML file
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  "
            };

            using (XmlWriter writer = XmlWriter.Create(outputXmlPath, settings))
            {
                // Start the XML document
                writer.WriteStartDocument();
                writer.WriteStartElement("FormFields");

                // Iterate over each form field (Field)
                foreach (Field field in pdfForm)
                {
                    // Field name (PartialName) and its current value
                    string fieldName = field.PartialName ?? string.Empty;
                    string fieldValue = field.Value?.ToString() ?? string.Empty;

                    // Write a <Field name="...">value</Field> element
                    writer.WriteStartElement("Field");
                    writer.WriteAttributeString("name", fieldName);
                    writer.WriteString(fieldValue);
                    writer.WriteEndElement(); // </Field>
                }

                // Close the root element and the document
                writer.WriteEndElement(); // </FormFields>
                writer.WriteEndDocument();
            }
        }

        Console.WriteLine($"Form field values have been exported to '{outputXmlPath}'.");
    }
}
