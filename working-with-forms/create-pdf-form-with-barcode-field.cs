using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "BarcodeForm.pdf";

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the rectangle where the barcode field will be placed
            // (llx, lly, urx, ury) in points
            Aspose.Pdf.Rectangle barcodeRect = new Aspose.Pdf.Rectangle(100, 500, 300, 560);

            // Create a barcode field on the page
            BarcodeField barcodeField = new BarcodeField(page, barcodeRect);
            barcodeField.PartialName = "ProductBarcode";   // field name
            barcodeField.AlternateName = "Enter product ID"; // tooltip
            barcodeField.Color = Aspose.Pdf.Color.Black;   // border/foreground color

            // Set the barcode symbology if needed (default is Code128)
            // barcodeField.Symbology = Aspose.Pdf.Forms.Symbology.Code128; // optional

            // Add a sample barcode value (this also makes the field read‑only)
            barcodeField.AddBarcode("123456789012");

            // Add the barcode field to the document's form collection
            doc.Form.Add(barcodeField);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with barcode field saved to '{outputPath}'.");
    }
}