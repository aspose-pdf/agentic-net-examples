using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";   // PDF containing a text field and a QR code field
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // ------------------------------------------------------------
            // 1. Locate the text field (the field whose value will drive the QR code)
            // ------------------------------------------------------------
            TextBoxField? textField = null;
            foreach (Field field in doc.Form)
            {
                if (field is TextBoxField tb && tb.PartialName == "TextField") // adjust name as needed
                {
                    textField = tb;
                    break;
                }
            }

            // ------------------------------------------------------------
            // 2. Locate the QR code field (a BarcodeField)
            // ------------------------------------------------------------
            BarcodeField? qrField = null;
            foreach (Field field in doc.Form)
            {
                if (field is BarcodeField bf && bf.PartialName == "QRField") // adjust name as needed
                {
                    qrField = bf;
                    break;
                }
            }

            if (textField == null || qrField == null)
            {
                Console.Error.WriteLine("Required fields not found in the document.");
                return;
            }

            // ------------------------------------------------------------
            // 3. Ensure the barcode field can be modified by JavaScript.
            //    The QR symbology must already be defined in the source PDF.
            // ------------------------------------------------------------
            // NOTE: BarcodeField.Symbology is read‑only; it must be set in the PDF designer.
            qrField.ReadOnly = false; // allow JavaScript to modify its value

            // ------------------------------------------------------------
            // 4. Attach JavaScript to the text field so that whenever its value changes,
            //    the QR code field is updated with the same value.
            //    The JavaScript runs on the client side (Adobe Reader) and sets the
            //    value of the barcode field, which automatically regenerates the QR code.
            // ------------------------------------------------------------
            string js = $"this.getField('{qrField.PartialName}').value = event.value;";
            textField.Actions.OnModifyCharacter = new JavascriptAction(js);

            // ------------------------------------------------------------
            // 5. Save the modified PDF
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'. QR code will update automatically when the text field changes.");
    }
}
