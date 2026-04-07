using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using System.Xml;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXml = "barcode_values.xml";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Prepare an XML document to hold barcode values
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement root = xmlDoc.CreateElement("Barcodes");
            xmlDoc.AppendChild(root);

            // Iterate over all form fields in the PDF
            foreach (Field field in pdfDoc.Form.Fields)
            {
                // Identify barcode fields
                if (field is BarcodeField barcodeField)
                {
                    // FullName uniquely identifies the field in the form
                    string fieldName = barcodeField.FullName;

                    // Value holds the barcode data (may be null)
                    string fieldValue = barcodeField.Value?.ToString() ?? string.Empty;

                    // Create an XML element for this barcode field
                    XmlElement barcodeElem = xmlDoc.CreateElement("Barcode");
                    barcodeElem.SetAttribute("Name", fieldName);
                    barcodeElem.InnerText = fieldValue;

                    // Append to root element
                    root.AppendChild(barcodeElem);
                }
            }

            // Persist the XML document to disk
            xmlDoc.Save(outputXml);
        }

        Console.WriteLine($"Barcode values exported to '{outputXml}'.");
    }
}