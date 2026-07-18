using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using System.Xml;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "barcode_values.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Prepare an XML document to hold barcode field data
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement root = xmlDoc.CreateElement("BarcodeFields");
            xmlDoc.AppendChild(root);

            // Iterate over all form fields in the PDF
            foreach (Field field in doc.Form.Fields)
            {
                // Process only barcode fields
                if (field is BarcodeField barcodeField)
                {
                    // Retrieve field identifier and its value
                    string fieldName = barcodeField.FullName ?? barcodeField.Name ?? "Unnamed";
                    string fieldValue = barcodeField.Value?.ToString() ?? string.Empty;

                    // Create an XML element for this barcode field
                    XmlElement fieldElement = xmlDoc.CreateElement("BarcodeField");
                    fieldElement.SetAttribute("Name", fieldName);
                    fieldElement.InnerText = fieldValue;

                    root.AppendChild(fieldElement);
                }
            }

            // Save the XML file
            xmlDoc.Save(outputPath);
        }

        Console.WriteLine($"Barcode field values exported to '{outputPath}'.");
    }
}