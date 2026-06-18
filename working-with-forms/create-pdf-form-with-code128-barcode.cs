using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

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
            // (left, bottom, right, top) coordinates
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Instantiate a BarcodeField on the page
            BarcodeField barcodeField = new BarcodeField(page, rect)
            {
                Name = "BarcodeField",          // internal field name
                PartialName = "Barcode",        // partial name for form hierarchy
                Color = Aspose.Pdf.Color.Black // barcode color
            };

            // Set a border for visual clarity (requires parent annotation)
            barcodeField.Border = new Border(barcodeField) { Width = 1 };

            // Generate a Code128 barcode from the numeric identifier
            barcodeField.AddBarcode(numericId);

            // Add the field to the document's form on page 1
            doc.Form.Add(barcodeField, 1);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with barcode field saved to '{outputPath}'.");
    }
}