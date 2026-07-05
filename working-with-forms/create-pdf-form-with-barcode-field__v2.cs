using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "barcode_form.pdf";
        const string numericId = "1234567890"; // identifier to encode

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the rectangle where the barcode field will be placed
            // (llx, lly, urx, ury) – lower‑left and upper‑right corners
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a BarcodeField on the page
            BarcodeField barcodeField = new BarcodeField(page, rect)
            {
                Name = "BarcodeField",          // field name
                PartialName = "BarcodeField",   // partial name (optional)
                AlternateName = "Barcode",      // tooltip shown in Acrobat
                ReadOnly = true                // barcode fields are read‑only after generation
            };

            // Generate a Code128 barcode from the numeric identifier
            barcodeField.AddBarcode(numericId);

            // Add the field to the document's form
            doc.Form.Add(barcodeField);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with barcode saved to '{outputPath}'.");
    }
}