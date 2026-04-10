using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "QrForm.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // 1. Text box field where the user will type data
            // -------------------------------------------------
            // Rectangle coordinates: lower‑left (llx, lly) and upper‑right (urx, ury)
            Aspose.Pdf.Rectangle txtRect = new Aspose.Pdf.Rectangle(100, 700, 300, 730);
            TextBoxField txtField = new TextBoxField(page, txtRect)
            {
                PartialName = "UserData",                     // field name used in the form
                AlternateName = "Enter data for QR code",     // tooltip shown in Acrobat
                MaxLen = 200                                  // optional character limit
            };
            doc.Form.Add(txtField);

            // -------------------------------------------------
            // 2. QR code field that will display the barcode
            // -------------------------------------------------
            // The BarcodeField constructor takes (Page, Rectangle)
            Aspose.Pdf.Rectangle qrRect = new Aspose.Pdf.Rectangle(100, 500, 300, 650);
            BarcodeField qrField = new BarcodeField(page, qrRect)
            {
                PartialName = "QrCode",               // field name
                Value = "SampleData"                  // placeholder value; can be replaced later
                // Symbology is read‑only; the default for a BarcodeField is QRCode.
            };
            doc.Form.Add(qrField);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with QR code field saved to '{outputPath}'.");
    }
}