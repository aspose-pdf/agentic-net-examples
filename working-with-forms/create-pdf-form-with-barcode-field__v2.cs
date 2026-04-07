using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "barcode_form.pdf";
        const string numericId = "1234567890";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to host the barcode field
            Page page = doc.Pages.Add();

            // Define the rectangle where the barcode will appear (llx, lly, urx, ury)
            // Use the fully qualified Aspose.Pdf.Rectangle to avoid ambiguity
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // Create a barcode field on the specified page and rectangle
            BarcodeField barcodeField = new BarcodeField(page, rect);
            barcodeField.Name = "BarcodeField";      // internal field name
            barcodeField.PartialName = "Barcode";    // optional display name
            barcodeField.AddBarcode(numericId);      // generate Code128 barcode from the numeric identifier
            barcodeField.ReadOnly = true;           // barcode fields become read‑only after AddBarcode

            // Add the barcode field to the document's form collection
            doc.Form.Add(barcodeField);

            // Save the resulting PDF form
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with barcode saved to '{outputPath}'.");
    }
}