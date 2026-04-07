using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class ExportFormDataToXml
{
    static void Main()
    {
        // Input PDF containing form fields
        const string inputPdfPath = "input_form.pdf";

        // Desired output XML file
        const string outputXmlPath = "form_data.xml";

        // Custom root element name required by the external system
        const string customRootName = "ExternalSystemFormData";

        // Verify the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // -----------------------------------------------------------------
            // NOTE:
            // The class `FormDataExportOptions` is not available in older versions of
            // Aspose.Pdf, which leads to a CS0246 compile error. To keep the example
            // functional across all supported versions we build the XML manually using
            // the standard .NET `System.Xml.Linq` API.
            // -----------------------------------------------------------------

            // Create the XML document with the required custom root element.
            XElement root = new XElement(customRootName);

            // Iterate over all form fields and add them to the XML.
            // Each field is represented as a <field> element with "name" and "value"
            // attributes. Adjust the structure if a different schema is required.
            foreach (var field in pdfDocument.Form.Fields)
            {
                string fieldName = field?.FullName ?? string.Empty;
                string fieldValue = field?.Value?.ToString() ?? string.Empty;

                XElement fieldElement = new XElement("field",
                    new XAttribute("name", fieldName),
                    new XAttribute("value", fieldValue));

                root.Add(fieldElement);
            }

            // Save the constructed XML to the specified path.
            XDocument xmlDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
            xmlDoc.Save(outputXmlPath);
        }

        Console.WriteLine($"Form data exported to XML with root '{customRootName}' at '{outputXmlPath}'.");
    }
}
