using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Output PDF file path
        const string outputPath = "BarcodeForm.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the barcode field will be placed
            // Rectangle constructor: (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a barcode field (Code128 by default) on the page
            BarcodeField barcodeField = new BarcodeField(page, rect)
            {
                // Set a unique name for the field (used for form processing)
                Name = "ProductBarcode",
                // Optional: tooltip shown in PDF viewers
                AlternateName = "Product Identifier"
            };

            // Set the barcode value (this also renders the barcode and makes the field read‑only)
            barcodeField.AddBarcode("1234567890123");

            // Add the barcode field to the document's form collection
            doc.Form.Add(barcodeField);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with barcode field saved to '{outputPath}'.");
    }
}