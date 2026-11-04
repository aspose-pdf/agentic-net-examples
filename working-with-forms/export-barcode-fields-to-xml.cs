using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputXmlPath = "barcodes.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Prepare an XML document to hold barcode values
            XDocument xmlDoc = new XDocument(new XElement("Barcodes"));

            // Ensure the PDF actually contains a form with fields
            var form = pdfDoc.Form;
            if (form != null && form.Fields != null)
            {
                foreach (var field in form.Fields)
                {
                    // Identify barcode fields by type (BarcodeField)
                    if (field is BarcodeField barcodeField)
                    {
                        // Use the field's name as an identifier
                        string fieldName = barcodeField.Name ?? "UnnamedBarcode";

                        // Retrieve the barcode value (as string). The Value property may be null.
                        string barcodeValue = barcodeField.Value?.ToString() ?? string.Empty;

                        // Add an element to the XML
                        XElement barcodeElement = new XElement("Barcode",
                            new XAttribute("name", fieldName),
                            barcodeValue);
                        xmlDoc.Root.Add(barcodeElement);
                    }
                }
            }

            // Save the XML file
            xmlDoc.Save(outputXmlPath);
            Console.WriteLine($"Exported barcode values to '{outputXmlPath}'.");
        }
    }
}
