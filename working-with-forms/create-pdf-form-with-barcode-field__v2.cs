using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Path for the output PDF
        const string outputPath = "barcode_form.pdf";

        // Numeric identifier to encode in the barcode
        const string numericId = "1234567890";

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the rectangle where the barcode field will be placed
            // (left, bottom, right, top) – fully qualified to avoid ambiguity
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // Create a BarcodeField on the document using the rectangle
            BarcodeField barcodeField = new BarcodeField(doc, rect)
            {
                // Set a name for the field (used to reference it later)
                Name = "BarcodeField",
                // Optional: tooltip shown in PDF viewers
                AlternateName = "Product Barcode"
            };

            // Add the barcode data (Code128) – this also makes the field read‑only
            barcodeField.AddBarcode(numericId);

            // Add the field to the form on page 1
            // Overload with page number ensures the field appears on the intended page
            doc.Form.Add(barcodeField, 1);

            // Save the PDF containing the barcode form field
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with barcode field saved to '{outputPath}'.");
    }
}