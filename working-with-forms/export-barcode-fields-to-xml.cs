using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class ExportBarcodeFieldsToXml
{
    static void Main()
    {
        // Input PDF containing form fields (including barcode fields)
        const string inputPdfPath = "input.pdf";

        // Output XML file that will contain the barcode field values
        const string outputXmlPath = "barcode_fields.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Create an XML document to hold the exported values
            XmlDocument xmlDoc = new XmlDocument();

            // Create the root element <BarcodeFields>
            XmlElement rootElement = xmlDoc.CreateElement("BarcodeFields");
            xmlDoc.AppendChild(rootElement);

            // Iterate over all form fields in the PDF
            foreach (Field field in pdfDocument.Form.Fields)
            {
                // Check if the field is a BarcodeField
                if (field is BarcodeField barcodeField)
                {
                    // Retrieve the field's full name (unique identifier) and its value
                    string fieldName = barcodeField.FullName ?? barcodeField.Name ?? "UnnamedBarcodeField";
                    string fieldValue = barcodeField.Value?.ToString() ?? string.Empty;

                    // Create an XML element for this barcode field
                    XmlElement fieldElement = xmlDoc.CreateElement("BarcodeField");

                    // Add name and value as attributes (you could also use child elements if preferred)
                    fieldElement.SetAttribute("Name", fieldName);
                    fieldElement.SetAttribute("Value", fieldValue);

                    // Append the field element to the root
                    rootElement.AppendChild(fieldElement);
                }
            }

            // Save the XML document to the specified file
            xmlDoc.Save(outputXmlPath);
        }

        Console.WriteLine($"Barcode field values exported to '{outputXmlPath}'.");
    }
}