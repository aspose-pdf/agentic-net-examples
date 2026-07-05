using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXml = "selected_fields.xml";

        // List of form field names that should be exported
        var fieldsToExport = new List<string>
        {
            "FirstName",
            "LastName",
            "Email"
        };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Create the root element for the XML document
            XElement root = new XElement("FormFields");

            foreach (string fieldName in fieldsToExport)
            {
                // Check if the form contains the field
                if (doc.Form.HasField(fieldName))
                {
                    // Retrieve the field (as a Field object)
                    Field field = doc.Form[fieldName] as Field;
                    if (field != null)
                    {
                        // Get the field value; convert null to empty string
                        string value = field.Value?.ToString() ?? string.Empty;

                        // Add an element with the field name and its value
                        XElement fieldElement = new XElement("Field",
                            new XAttribute("Name", fieldName),
                            new XAttribute("Value", value));
                        root.Add(fieldElement);
                    }
                }
                else
                {
                    Console.WriteLine($"Field not found: {fieldName}");
                }
            }

            // Save the XML document
            XDocument xmlDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
            xmlDoc.Save(outputXml);
            Console.WriteLine($"Selected form fields exported to '{outputXml}'.");
        }
    }
}