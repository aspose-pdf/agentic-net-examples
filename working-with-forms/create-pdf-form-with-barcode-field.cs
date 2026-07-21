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

            // Define the position and size of the barcode field
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a BarcodeField on the specified page and rectangle
            BarcodeField barcodeField = new BarcodeField(page, rect);

            // Set a name for the field (optional but useful for identification)
            barcodeField.Name = "ProductBarcode";

            // Add a Code128 barcode with the desired value
            // This also makes the field read‑only after the barcode is generated
            barcodeField.AddBarcode("123456789012");

            // Add the barcode field to the document's form collection
            doc.Form.Add(barcodeField);

            // Save the PDF containing the barcode form field
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with barcode form saved to '{outputPath}'.");
    }
}