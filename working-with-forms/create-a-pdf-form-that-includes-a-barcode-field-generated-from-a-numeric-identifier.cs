using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

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

            // Define the position and size of the barcode field
            // Fully qualified type to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Instantiate a BarcodeField on the page
            BarcodeField barcodeField = new BarcodeField(page, rect);

            // Set a name for the field (used in form data) and a tooltip
            barcodeField.PartialName = "BarcodeField";
            barcodeField.AlternateName = "Product Barcode";

            // Generate a Code128 barcode from the numeric identifier
            barcodeField.AddBarcode(numericId);

            // Add the field to the document's form collection
            doc.Form.Add(barcodeField);

            // Save the PDF containing the barcode form field
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with barcode field saved to '{outputPath}'.");
    }
}