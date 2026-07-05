using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "barcode_form.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page (evaluation mode allows up to 4 pages)
            Page page = doc.Pages.Add();

            // Define the rectangle for the barcode field
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the barcode field on the page
            BarcodeField barcodeField = new BarcodeField(page, rect)
            {
                Name = "ProductBarcode",
                AlternateName = "Enter product identifier"
            };

            // Add a Code128 barcode value
            barcodeField.AddBarcode("1234567890");

            // Add the field to the form, specifying the page number (1‑based)
            doc.Form.Add(barcodeField, 1);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with barcode saved to '{outputPath}'.");
    }
}