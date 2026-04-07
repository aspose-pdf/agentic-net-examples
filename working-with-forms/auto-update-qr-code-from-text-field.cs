using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";      // PDF containing a text field and a QR code field
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure form fields are recalculated when any field changes
            doc.Form.AutoRecalculate = true;

            // Retrieve the text field (the source of the data)
            TextBoxField txtField = doc.Form["txtField"] as TextBoxField;
            if (txtField == null)
            {
                Console.Error.WriteLine("Text field 'txtField' not found.");
                return;
            }

            // Retrieve the barcode field that will display the QR code
            BarcodeField qrField = doc.Form["qrField"] as BarcodeField;
            if (qrField == null)
            {
                Console.Error.WriteLine("Barcode field 'qrField' not found.");
                return;
            }

            // The barcode field's symbology is read‑only; it must be defined in the PDF template.
            // Remove the illegal assignment to Symbology.
            // qrField.Symbology = Symbology.QRCode; // <-- removed (read‑only)

            // Prevent manual editing of the barcode field
            qrField.ReadOnly = true;

            // Attach JavaScript to the text field so that the QR code updates automatically.
            // The script copies the current value of the text field into the barcode field.
            string js = "this.getField('qrField').value = event.value;";
            txtField.Actions.OnModifyCharacter = new JavascriptAction(js);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with auto‑updating QR code: {outputPath}");
    }
}
