using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths for the input template (optional) and output PDF
        const string outputPath = "BarcodeDocument.pdf";

        // Generate a unique value for the barcode (e.g., GUID without hyphens)
        string uniqueCode = Guid.NewGuid().ToString("N");

        // Create a new PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document())
        {
            // Add a blank page to the document
            Aspose.Pdf.Page page = doc.Pages.Add();

            // Define the rectangle where the barcode field will be placed
            // (llx, lly, urx, ury) in points (1/72 inch)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a BarcodeField on the page with the specified rectangle
            Aspose.Pdf.Forms.BarcodeField barcodeField = new Aspose.Pdf.Forms.BarcodeField(page, rect);

            // Optionally set a name for the field (useful for later reference)
            barcodeField.Name = "UniqueBarcode";

            // Add the barcode field to the document's form collection
            doc.Form.Add(barcodeField);

            // Populate the barcode field with the unique code.
            // AddBarcode generates a Code128 barcode and makes the field read‑only.
            barcodeField.AddBarcode(uniqueCode);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with barcode saved to '{outputPath}'. Barcode value: {uniqueCode}");
    }
}