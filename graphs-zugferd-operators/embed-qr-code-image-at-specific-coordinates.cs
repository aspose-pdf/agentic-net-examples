using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_qr.pdf";
        const string qrText = "https://example.com";

        // Desired placement rectangle (lower‑left and upper‑right corners)
        const double llx = 100; // X of lower‑left
        const double lly = 500; // Y of lower‑left
        const double urx = 200; // X of upper‑right
        const double ury = 600; // Y of upper‑right

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF and embed the QR code using a form field (no Barcode namespace required)
        using (Document doc = new Document(inputPdf))
        {
            // 1‑based page indexing (first page)
            Page page = doc.Pages[1];

            // Create a TextBoxField that will hold the QR code
            var qrField = new TextBoxField(page, new Aspose.Pdf.Rectangle(llx, lly, urx, ury))
            {
                PartialName = "qrCodeField"
            };

            // Add the field to the document's form (page number is 1‑based)
            doc.Form.Add(qrField, 1);

            // Draw the QR code into the field. AddBarcode automatically chooses the appropriate symbology (QR).
            qrField.AddBarcode(qrText);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"QR code embedded and saved to '{outputPdf}'.");
    }
}
