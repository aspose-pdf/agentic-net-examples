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
        const string xmlPath = "data.xml";
        const string pdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load XML containing barcode definitions
        XDocument xmlDoc = XDocument.Load(xmlPath);

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // Add a single page (you can add more if required)
            Page page = pdfDoc.Pages.Add();

            // No need to assign pdfDoc.Form – it is created automatically and is read‑only.

            // Iterate over each <Barcode> element in the XML
            // Expected attributes: Type (e.g., Code128), Value, X, Y, Width, Height
            foreach (XElement barcodeElem in xmlDoc.Root.Elements("Barcode"))
            {
                // Parse attributes with defaults
                string valueStr = (string)barcodeElem.Attribute("Value") ?? "";
                double xPos = (double?)barcodeElem.Attribute("X") ?? 0;
                double yPos = (double?)barcodeElem.Attribute("Y") ?? 0;
                double width = (double?)barcodeElem.Attribute("Width") ?? 150;
                double height = (double?)barcodeElem.Attribute("Height") ?? 50;

                // Create a rectangle for the barcode field
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(xPos, yPos, xPos + width, yPos + height);

                // Create a TextBoxField that will hold the barcode
                TextBoxField barcodeField = new TextBoxField(page, rect)
                {
                    // Give each field a unique name (optional but recommended)
                    PartialName = $"Barcode_{Guid.NewGuid():N}"
                };

                // Add the field to the document's form (page number is 1‑based)
                pdfDoc.Form.Add(barcodeField, 1);

                // Draw the barcode into the field. The default symbology is Code128.
                // If a different symbology is required, the field's Barcode property can be used,
                // but for this example we rely on the default.
                barcodeField.AddBarcode(valueStr);
            }

            // Save the resulting PDF
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF with barcodes saved to '{pdfPath}'.");
    }
}
