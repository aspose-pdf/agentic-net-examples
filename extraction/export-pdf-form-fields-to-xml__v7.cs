using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "FormF.pdf";
        const string outputXml = "FormF_fields.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal (document-disposal-with-using rule)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Root element for the XML output
            XElement fieldsRoot = new XElement("Fields");

            // Iterate over all form fields using the Forms API (iterate-form-fields-via-formfields-collection rule)
            foreach (Field field in pdfDoc.Form.Fields)
            {
                // Field name may be stored in FullName or PartialName
                string fieldName = field.FullName ?? field.PartialName ?? string.Empty;
                string fieldType = field.GetType().Name;
                string fieldValue = field.Value?.ToString() ?? string.Empty;

                // Create an XML element representing the field
                XElement fieldElement = new XElement("Field",
                    new XAttribute("Name", fieldName),
                    new XAttribute("Type", fieldType),
                    new XAttribute("Value", fieldValue));

                fieldsRoot.Add(fieldElement);
            }

            // Build the XDocument and save to the specified XML file
            XDocument xmlDoc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                fieldsRoot);

            xmlDoc.Save(outputXml);
        }

        Console.WriteLine($"Form field data exported to '{outputXml}'.");
    }
}
