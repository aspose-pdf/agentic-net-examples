using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing; // for other drawing types if needed

class Program
{
    static void Main()
    {
        const string outputPath = "barcode_form.pdf";
        const string numericId = "1234567890"; // identifier to encode as barcode

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            // Ensure there is at least one page
            Page page = doc.Pages.Add();

            // Define the rectangle where the barcode field will be placed
            // Use fully qualified type to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle barcodeRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the barcode field on the document
            BarcodeField barcodeField = new BarcodeField(doc, barcodeRect)
            {
                Name = "BarcodeField",          // internal field name
                PartialName = "Barcode",        // optional display name
                Color = Aspose.Pdf.Color.Black // optional border/color
            };

            // Generate a Code128 barcode from the numeric identifier
            barcodeField.AddBarcode(numericId);

            // Add the field to the form on page 1
            doc.Form.Add(barcodeField, 1);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with barcode saved to '{outputPath}'.");
    }
}