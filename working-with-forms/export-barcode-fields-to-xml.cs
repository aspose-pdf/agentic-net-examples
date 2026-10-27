using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xmlPath = "barcode_values.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Create an XML writer to generate the output file
            using (XmlWriter writer = XmlWriter.Create(xmlPath, new XmlWriterSettings { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Barcodes");

                // Iterate over all form fields in the PDF
                foreach (Field field in doc.Form.Fields)
                {
                    // Process only barcode fields
                    if (field is BarcodeField barcodeField)
                    {
                        string fieldName = barcodeField.FullName; // name of the barcode field
                        string barcodeValue = barcodeField.Value; // extracted barcode value

                        writer.WriteStartElement("Barcode");
                        writer.WriteAttributeString("Name", fieldName);
                        writer.WriteString(barcodeValue ?? string.Empty);
                        writer.WriteEndElement(); // </Barcode>
                    }
                }

                writer.WriteEndElement(); // </Barcodes>
                writer.WriteEndDocument();
            }
        }

        Console.WriteLine($"Barcode values exported to '{xmlPath}'.");
    }
}