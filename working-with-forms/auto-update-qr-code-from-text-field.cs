using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // for JavascriptAction

class Program
{
    static void Main()
    {
        const string inputPath = "form.pdf";
        const string outputPath = "form_with_qr_update.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the text field and the QR code barcode field by their names
            TextBoxField txtField = doc.Form["txtData"] as TextBoxField;
            BarcodeField qrField = doc.Form["qrCode"] as BarcodeField;

            if (txtField == null || qrField == null)
            {
                Console.Error.WriteLine("Required fields 'txtData' or 'qrCode' not found in the form.");
                return;
            }

            // The barcode field is expected to be pre‑configured as a QR code in the source PDF.
            // Add JavaScript that updates the QR code value whenever the form is recalculated.
            qrField.Actions.OnCalculate = new JavascriptAction("event.value = this.getField('txtData').value;");

            // Enable automatic recalculation of form fields. When the PDF is opened
            // the JavaScript will run and the QR code will reflect the current text.
            doc.Form.AutoRecalculate = true;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}