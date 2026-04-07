using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "barcode_form.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to host the barcode field
            Page page = doc.Pages.Add();

            // Define the position and size of the barcode field (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Instantiate a BarcodeField on the page
            BarcodeField barcodeField = new BarcodeField(page, rect);
            barcodeField.PartialName = "ProductBarcode"; // field name
            barcodeField.AddBarcode("987654321012");    // barcode data (Code128 by default)

            // Add the barcode field to the document's form collection
            doc.Form.Add(barcodeField);

            // Save the PDF containing the barcode form field
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with barcode saved to '{outputPath}'.");
    }
}