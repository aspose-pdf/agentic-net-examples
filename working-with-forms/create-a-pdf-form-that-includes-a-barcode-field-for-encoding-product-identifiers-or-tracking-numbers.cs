using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "barcode_form.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (first page will have index 1)
            Page page = doc.Pages.Add();

            // Define the rectangle where the barcode field will be placed
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a BarcodeField on the page
            BarcodeField barcodeField = new BarcodeField(page, rect)
            {
                // Set a logical name for the field (used when extracting form data)
                Name = "ProductBarcode",
                // Optional: set a tooltip (alternate name) shown in PDF viewers
                AlternateName = "Enter product identifier"
            };

            // Add a Code128 barcode with the desired value
            // This makes the field read‑only and renders the barcode graphic
            barcodeField.AddBarcode("123456789012");

            // Add the barcode field to the document's form on page 1
            // The overload Add(Field, int) places the field on the specified page
            doc.Form.Add(barcodeField, 1);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with barcode field saved to '{outputPath}'.");
    }
}