using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "BarcodeForm.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the rectangle where the barcode field will appear
            // (llx, lly, urx, ury) in points
            Aspose.Pdf.Rectangle barcodeRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // Create a BarcodeField on the page
            BarcodeField barcodeField = new BarcodeField(page, barcodeRect)
            {
                Name = "ProductBarcode",          // field name
                PartialName = "ProductBarcode",   // optional, same as Name
                Color = Color.Black,              // border/color of the field
                ReadOnly = true                   // make the field read‑only after barcode is set
            };

            // Generate a Code128 barcode with the desired data
            barcodeField.AddBarcode("1234567890123");

            // Add the field to the document's form collection
            doc.Form.Add(barcodeField);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with barcode field saved to '{outputPath}'.");
    }
}