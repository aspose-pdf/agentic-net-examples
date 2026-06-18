using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string xmlPath = "data.xml";
        const string outputPdf = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML file and convert it to a PDF document.
        XmlLoadOptions loadOptions = new XmlLoadOptions();
        using (Document pdfDoc = new Document(xmlPath, loadOptions))
        {
            // Example XML format:
            // <Barcodes>
            //   <Item name="Item1">1234567890</Item>
            //   <Item name="Item2">ABCDEF</Item>
            // </Barcodes>
            XDocument xDoc = XDocument.Load(xmlPath);
            var items = xDoc.Root?.Elements("Item");
            if (items != null)
            {
                // Position barcodes vertically on the first page.
                int yPos = 700; // Starting Y coordinate.
                foreach (var item in items)
                {
                    string fieldName = (string)item.Attribute("name") ?? "Barcode";
                    string barcodeValue = item.Value ?? string.Empty;

                    // Define the rectangle where the barcode will be placed.
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, yPos - 50, 250, yPos);

                    // Create a barcode field on the first page.
                    BarcodeField barcodeField = new BarcodeField(pdfDoc.Pages[1], rect)
                    {
                        Name = fieldName,
                        ReadOnly = true
                    };

                    // Generate a Code128 barcode with the provided value.
                    barcodeField.AddBarcode(barcodeValue);

                    // Move down for the next barcode.
                    yPos -= 100;
                }
            }

            // Save the resulting PDF.
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with barcodes saved to '{outputPdf}'.");
    }
}