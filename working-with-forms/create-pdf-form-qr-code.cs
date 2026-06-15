using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

namespace CreatePdfFormQrCode
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a sample PDF file (self‑contained example).
            using (Document createDoc = new Document())
            {
                Page createPage = createDoc.Pages.Add();
                createDoc.Save("input.pdf");
            }

            // Step 2: Open the sample PDF and add a QR‑code field.
            using (Document doc = new Document("input.pdf"))
            {
                // Aspose.Pdf uses 1‑based page indexing.
                Page page = doc.Pages[1];

                // Define the rectangle where the QR code will appear.
                // Rectangle(left, bottom, right, top) – all values are in points.
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

                // Create the QR code barcode field using the (Page, Rectangle) constructor.
                BarcodeField qrField = new BarcodeField(page, rect);
                qrField.Name = "QRCodeField";
                qrField.PartialName = "QRCodeField";
                qrField.Value = "https://example.com"; // data encoded in the QR code

                // Add the field to the document form.
                doc.Form.Add(qrField);

                // Save the resulting PDF.
                doc.Save("output.pdf");
            }
        }
    }
}
