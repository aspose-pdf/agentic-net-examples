using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class ExportBarcodeFieldsToXml
{
    static void Main()
    {
        // Input PDF containing barcode form fields
        const string inputPdfPath = "input.pdf";
        // Output XML file with barcode field values
        const string outputXmlPath = "barcode_fields.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Create an XML document to hold the exported data
            XmlDocument xmlDoc = new XmlDocument();

            // Create the root element <BarcodeFields>
            XmlElement rootElement = xmlDoc.CreateElement("BarcodeFields");
            xmlDoc.AppendChild(rootElement);

            // Iterate over all form fields in the PDF
            foreach (Field formField in pdfDocument.Form.Fields)
            {
                // Check if the field is a BarcodeField
                if (formField is BarcodeField barcodeField)
                {
                    // Retrieve the full name of the field (unique identifier)
                    string fieldName = barcodeField.FullName ?? string.Empty;
                    // Retrieve the value stored in the barcode field
                    string fieldValue = barcodeField.Value ?? string.Empty;

                    // Create an XML element <BarcodeField name="..." value="..."/>
                    XmlElement fieldElement = xmlDoc.CreateElement("BarcodeField");
                    fieldElement.SetAttribute("name", fieldName);
                    fieldElement.SetAttribute("value", fieldValue);

                    // Append the element to the root
                    rootElement.AppendChild(fieldElement);
                }
            }

            // Save the XML document to the specified file
            xmlDoc.Save(outputXmlPath);
        }

        Console.WriteLine($"Barcode field values exported to '{outputXmlPath}'.");
    }
}