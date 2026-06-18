using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // PDF containing a TextBoxField named "txtInput" and a BarcodeField named "qrField"
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the text box field (the source of the data)
            TextBoxField txtField = doc.Form["txtInput"] as TextBoxField;
            // Retrieve the barcode field that will display the QR code
            BarcodeField qrField = doc.Form["qrField"] as BarcodeField;

            if (txtField == null || qrField == null)
            {
                Console.Error.WriteLine("Required fields not found in the PDF form.");
                return;
            }

            // NOTE: BarcodeField.Symbology is read‑only. The QR code symbology must be
            // pre‑configured in the PDF template. We only need to ensure the field can be
            // updated by JavaScript.
            qrField.ReadOnly = false;

            // Attach JavaScript to the text field: whenever a character is modified,
            // set the barcode field's value to the current text field value.
            // The JavaScript runs inside the PDF viewer and updates the QR code automatically.
            txtField.Actions.OnModifyCharacter = new JavascriptAction(
                "var txt = event.value; " +                     // current text box value
                "var qr = this.getField('qrField'); " +        // reference to the barcode field
                "qr.value = txt; " +                           // update barcode field value
                "qr.recalculate();");                          // trigger barcode regeneration

            // Optionally enable automatic recalculation for the whole form (default is true)
            doc.Form.AutoRecalculate = true;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'. QR code will update automatically when the text field changes.");
    }
}
