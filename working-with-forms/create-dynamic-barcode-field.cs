using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Output PDF file path
        const string outputPath = "barcode_output.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the barcode field will be placed
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a BarcodeField on the specified page and rectangle
            BarcodeField barcodeField = new BarcodeField(page, rect);

            // Generate dynamic data for the barcode (e.g., a GUID substring)
            string dynamicData = Guid.NewGuid().ToString("N").Substring(0, 12); // 12‑character Code128 value

            // Populate the barcode field with the dynamic data
            // This sets the field value and makes it read‑only
            barcodeField.AddBarcode(dynamicData);

            // Optionally assign a name to the field for identification
            barcodeField.Name = "UniqueBarcode";

            // Add the barcode field to the document's form collection (not directly to page annotations)
            doc.Form.Add(barcodeField);

            // Save the PDF document to the specified path
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with barcode saved to '{outputPath}'.");
    }
}
