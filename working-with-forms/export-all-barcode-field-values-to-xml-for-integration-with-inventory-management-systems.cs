using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF containing barcode fields
        const string outputXmlPath = "barcodeValues.xml"; // target XML file

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Prepare an XML document to hold barcode field data
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement root = xmlDoc.CreateElement("BarcodeFields");
            xmlDoc.AppendChild(root);

            // Iterate over all form fields and pick only BarcodeField instances
            foreach (Field field in pdfDoc.Form.Fields)
            {
                if (field is BarcodeField barcodeField)
                {
                    // Create an element for each barcode field
                    XmlElement fieldElem = xmlDoc.CreateElement("BarcodeField");

                    // Use the full name if available, otherwise fallback to the short name
                    string fieldName = barcodeField.FullName ?? barcodeField.Name ?? string.Empty;
                    fieldElem.SetAttribute("Name", fieldName);

                    // Export the current value of the barcode field
                    string fieldValue = barcodeField.Value?.ToString() ?? string.Empty;
                    fieldElem.SetAttribute("Value", fieldValue);

                    root.AppendChild(fieldElem);
                }
            }

            // Save the constructed XML (lifecycle: save)
            xmlDoc.Save(outputXmlPath);
        }

        Console.WriteLine($"Barcode field values exported to '{outputXmlPath}'.");
    }
}