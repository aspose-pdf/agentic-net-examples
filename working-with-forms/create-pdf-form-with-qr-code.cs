using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "qr_form.pdf";
        const string qrData = "https://example.com";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the QR code field will appear
            // (llx, lly, urx, ury) in points
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create a BarcodeField (QR code) on the page
            BarcodeField qrField = new BarcodeField(page, rect)
            {
                Name = "QRCodeField",          // Field name
                Value = qrData,                // Data to encode in the QR code
                Color = Aspose.Pdf.Color.Black // Optional: set barcode color
            };

            // Add the field to the document's form
            doc.Form.Add(qrField);

            // Save the PDF with the QR code field
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with QR code saved to '{outputPath}'.");
    }
}