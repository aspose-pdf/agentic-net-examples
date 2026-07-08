using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using System.Xml;

class ExportBarcodeValues
{
    static void Main()
    {
        // Input PDF containing barcode form fields
        const string inputPdfPath = "input.pdf";

        // Output XML file that will hold the exported barcode values
        const string outputXmlPath = "barcode_values.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Prepare an XML document to store the barcode values
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement rootElement = xmlDoc.CreateElement("Barcodes");
            xmlDoc.AppendChild(rootElement);

            // Iterate over all form fields in the PDF
            foreach (Field field in pdfDocument.Form.Fields)
            {
                // Identify barcode fields
                if (field is BarcodeField barcodeField)
                {
                    // Use the full field name if available; otherwise fall back to the short name
                    string fieldName = !string.IsNullOrEmpty(barcodeField.FullName)
                        ? barcodeField.FullName
                        : barcodeField.Name ?? "UnnamedBarcode";

                    // Retrieve the barcode value (the data encoded in the barcode)
                    string barcodeValue = barcodeField.Value?.ToString() ?? string.Empty;

                    // Create an XML element for this barcode field
                    XmlElement barcodeElement = xmlDoc.CreateElement("Barcode");
                    barcodeElement.SetAttribute("Name", fieldName);
                    barcodeElement.InnerText = barcodeValue;

                    // Append the element to the root
                    rootElement.AppendChild(barcodeElement);
                }
            }

            // Save the constructed XML to the specified file
            xmlDoc.Save(outputXmlPath);
        }

        Console.WriteLine($"Barcode values exported to '{outputXmlPath}'.");
    }
}