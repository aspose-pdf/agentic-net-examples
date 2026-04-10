using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string xmlPath = "input.xml";
        const string pdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML document
        XDocument xDoc = XDocument.Load(xmlPath);

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // Add a single page (you can add more pages if needed)
            pdfDoc.Pages.Add();
            Page page = pdfDoc.Pages[1];

            // Starting position for the first barcode
            double startX = 50;
            double startY = 750;
            double barcodeWidth = 200;
            double barcodeHeight = 50;
            double verticalSpacing = 70;

            // Iterate over each <Barcode> element in the XML
            foreach (XElement barcodeElem in xDoc.Descendants("Barcode"))
            {
                // Expected XML format:
                // <Barcode id="Field1">1234567890</Barcode>
                string fieldId = (string)barcodeElem.Attribute("id") ?? Guid.NewGuid().ToString();
                string barcodeValue = barcodeElem.Value?.Trim() ?? string.Empty;

                // Define the rectangle where the barcode will be placed
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                    startX,
                    startY - barcodeHeight,
                    startX + barcodeWidth,
                    startY);

                // Create the barcode field on the page
                BarcodeField barcodeField = new BarcodeField(page, rect);
                barcodeField.Name = fieldId;               // optional: set a name for the field
                barcodeField.AddBarcode(barcodeValue);     // generate Code128 barcode

                // Add the barcode annotation to the page
                page.Annotations.Add(barcodeField);

                // Move down for the next barcode
                startY -= verticalSpacing;
                if (startY < 100) // simple page overflow handling
                {
                    pdfDoc.Pages.Add();
                    page = pdfDoc.Pages[pdfDoc.Pages.Count];
                    startY = 750;
                }
            }

            // Save the resulting PDF
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"Barcodes generated and saved to '{pdfPath}'.");
    }
}