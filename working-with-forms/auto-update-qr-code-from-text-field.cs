using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";   // PDF containing a text field "txtField" and a barcode field "qrField"
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure form recalculation is enabled (default is true)
            doc.Form.AutoRecalculate = true;

            // Retrieve the text box field that the user will edit
            TextBoxField txtField = doc.Form["txtField"] as TextBoxField;
            // Retrieve the barcode field that will display the QR code
            BarcodeField qrField = doc.Form["qrField"] as BarcodeField;

            if (txtField == null || qrField == null)
            {
                Console.Error.WriteLine("Required fields not found in the PDF.");
                return;
            }

            // NOTE: Symbology and ECC are read‑only properties of an existing BarcodeField.
            // The PDF template should already define the field as a QR Code with the desired error correction level.
            // Therefore we do NOT assign to qrField.Symbology or qrField.ECC here.

            // Attach JavaScript to the text field so that when its value changes,
            // the QR code field is updated automatically.
            // The JavaScript runs in the PDF viewer context.
            string js = "this.getField('qrField').value = event.value;";
            txtField.Actions.OnModifyCharacter = new JavascriptAction(js);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with auto‑updating QR code saved to '{outputPath}'.");
    }
}
