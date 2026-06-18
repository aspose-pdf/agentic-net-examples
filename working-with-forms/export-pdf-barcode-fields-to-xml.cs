using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputXml = "barcode_values.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Prepare an XML document to hold barcode values
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement root = xmlDoc.CreateElement("Barcodes");
            xmlDoc.AppendChild(root);

            // Iterate through all form fields in the PDF
            foreach (Field field in doc.Form.Fields)
            {
                // Process only barcode fields
                if (field is BarcodeField barcodeField)
                {
                    // Use the full field name if available; otherwise fallback to partial name
                    string fieldName = barcodeField.FullName ?? barcodeField.Name ?? "UnnamedBarcode";

                    // Retrieve the barcode value (as string)
                    string value = barcodeField.Value?.ToString() ?? string.Empty;

                    // Create an XML element for this barcode field
                    XmlElement barcodeElem = xmlDoc.CreateElement("Barcode");
                    barcodeElem.SetAttribute("Name", fieldName);
                    barcodeElem.InnerText = value;

                    root.AppendChild(barcodeElem);
                }
            }

            // Write the XML document to the output file
            using (FileStream fs = new FileStream(outputXml, FileMode.Create, FileAccess.Write))
            {
                xmlDoc.Save(fs);
            }
        }

        Console.WriteLine($"Barcode values exported to {outputXml}");
    }
}