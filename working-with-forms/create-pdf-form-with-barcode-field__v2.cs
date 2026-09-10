using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Paths for the output PDF
        const string outputPath = "BarcodeForm.pdf";

        // Numeric identifier to encode as a barcode
        const string numericId = "1234567890";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the barcode field will be placed
            // (left, bottom, width, height) – using the Aspose.Pdf.Rectangle type
            Aspose.Pdf.Rectangle barcodeRect = new Aspose.Pdf.Rectangle(100, 500, 300, 150);

            // Create a BarcodeField on the page
            BarcodeField barcodeField = new BarcodeField(page, barcodeRect)
            {
                // Set a name for the field (used to reference it later)
                Name = "BarcodeField",
                // Optional: set a tooltip (alternate name)
                AlternateName = "Product Barcode"
            };

            // Generate a Code128 barcode from the numeric identifier
            // This also makes the field read‑only automatically
            barcodeField.AddBarcode(numericId);

            // Add the barcode field to the document's form
            // The Form object is obtained from the document
            doc.Form.Add(barcodeField);

            // Save the PDF with the barcode form field
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with barcode saved to '{outputPath}'.");
    }
}