using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Paths for the generated PDF
        const string outputPath = "QrForm.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // 1. Text box where the user can enter data
            // -------------------------------------------------
            // Define the rectangle for the text box (lower‑left x, y, upper‑right x, y)
            Aspose.Pdf.Rectangle txtRect = new Aspose.Pdf.Rectangle(100, 400, 300, 450);
            TextBoxField inputField = new TextBoxField(page, txtRect)
            {
                Name = "UserInput",
                // Optional: set a default placeholder value
                Value = "Enter data here"
            };
            // The field is automatically added to the page when constructed

            // -------------------------------------------------
            // 2. QR code field that will display the entered data
            // -------------------------------------------------
            // Define the rectangle for the QR code field
            Aspose.Pdf.Rectangle qrRect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);
            BarcodeField qrField = new BarcodeField(page, qrRect)
            {
                Name = "QrCodeField",
                // Set an initial value; this can be updated later programmatically
                Value = "SampleData"
                // The ECC (error correction level) for QR codes is read‑only and defaults to 0 (L)
                // Symbology is also read‑only; the field is treated as a QR code when the value is set
            };

            // -------------------------------------------------
            // Save the PDF with the form fields
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with QR code field saved to '{outputPath}'.");
    }
}