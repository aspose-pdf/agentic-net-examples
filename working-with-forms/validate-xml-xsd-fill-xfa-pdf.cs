using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string xmlPath = "data.xml";
        const string xsdPath = "schema.xsd";
        const string pdfTemplatePath = "template.pdf";
        const string outputPdfPath = "filled.pdf";

        if (!File.Exists(xmlPath) || !File.Exists(xsdPath) || !File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine("Required file missing.");
            return;
        }

        // Validate XML against the XSD schema
        bool isValid = true;
        XmlReaderSettings settings = new XmlReaderSettings();
        settings.Schemas.Add(null, xsdPath);
        settings.ValidationType = ValidationType.Schema;
        settings.ValidationEventHandler += (sender, args) =>
        {
            Console.Error.WriteLine($"Validation error: {args.Message}");
            isValid = false;
        };

        using (var reader = XmlReader.Create(xmlPath, settings))
        {
            try
            {
                while (reader.Read()) { } // forces validation while reading
            }
            catch (XmlException ex)
            {
                Console.Error.WriteLine($"XML parsing error: {ex.Message}");
                return;
            }
        }

        if (!isValid)
        {
            Console.Error.WriteLine("XML validation failed. Import aborted.");
            return;
        }

        // Load XML data into a dictionary (field name -> value)
        var fieldValues = new Dictionary<string, string>();
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlPath);
        // Expected format: <Fields><Field name="FieldName">Value</Field>...</Fields>
        var nodes = xmlDoc.SelectNodes("//Field[@name]");
        if (nodes != null)
        {
            foreach (XmlNode node in nodes)
            {
                var nameAttr = node.Attributes["name"];
                if (nameAttr != null)
                {
                    fieldValues[nameAttr.Value] = node.InnerText;
                }
            }
        }

        // Fill the PDF form fields
        using (Document pdfDoc = new Document(pdfTemplatePath))
        {
            foreach (var kvp in fieldValues)
            {
                // Retrieve the form field using the Forms API. The indexer returns a WidgetAnnotation,
                // so we need to cast it to Field (or a derived type) before accessing the Value property.
                Field? field = pdfDoc.Form[kvp.Key] as Field;
                if (field != null)
                {
                    field.Value = kvp.Value;
                }
                else
                {
                    Console.WriteLine($"Warning: PDF does not contain field '{kvp.Key}'.");
                }
            }

            pdfDoc.Save(outputPdfPath); // saves as PDF
        }

        Console.WriteLine($"PDF form filled and saved to '{outputPdfPath}'.");
    }
}
