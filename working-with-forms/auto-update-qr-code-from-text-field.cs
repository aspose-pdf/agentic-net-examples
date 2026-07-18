using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Enable automatic recalculation of form fields.
            doc.Form.AutoRecalculate = true;

            // Guard against missing form collection.
            if (doc.Form == null || doc.Form.Fields == null)
            {
                Console.Error.WriteLine("The document does not contain a form.");
                return;
            }

            // Retrieve the text field that drives the QR code.
            TextBoxField txtField = doc.Form["TextField"] as TextBoxField;
            // Retrieve the QR code field (a BarcodeField).
            BarcodeField qrField = doc.Form["QRField"] as BarcodeField;

            if (txtField == null)
            {
                Console.Error.WriteLine("Text field 'TextField' not found.");
                return;
            }

            if (qrField == null)
            {
                Console.Error.WriteLine("Barcode field 'QRField' not found.");
                return;
            }

            // The QR symbology must already be defined in the PDF template.
            // BarcodeField.Symbology is read‑only, so we only update the value.
            string js = @"
                var txt = event.value;
                var qr  = this.getField('QRField');
                qr.value = txt;
            ";
            txtField.Actions.OnModifyCharacter = new JavascriptAction(js);

            // Save the updated PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with dynamic QR code: {outputPath}");
    }
}
